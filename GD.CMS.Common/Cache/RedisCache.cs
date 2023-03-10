using Castle.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Threading;

namespace GD.CMS.Common
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public class RedisCache : ICache
    {
        /// <summary>
        /// 默认构造函数
        /// 注：使用默认配置，即localhost:6379,无密码
        /// </summary>
        public RedisCache()
        {
            _databaseIndex = 0;
            string config ="";
            _redisConnection = ConnectionMultiplexer.Connect(config);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config">配置字符串</param>
        /// <param name="databaseIndex">数据库索引</param>
        public RedisCache(string config, int databaseIndex = 0)
        {
            _databaseIndex = databaseIndex;
            _redisConnection = ConnectionMultiplexer.Connect(config);
        }

        private ConnectionMultiplexer _redisConnection { get; }
        private IDatabase _db { get => _redisConnection.GetDatabase(_databaseIndex); }
        private int _databaseIndex { get; }
        public bool ContainsKey(string key)
        {
            return _db.KeyExists(key);
        }

        public object GetCache(string key)
        {
            object value = null;
            var redisValue = _db.StringGet(key);
            if (!redisValue.HasValue)
                return null;
            ValueInfoEntry valueEntry = redisValue.ToString().ToObject<ValueInfoEntry>();
            if (valueEntry.TypeName == typeof(string).AssemblyQualifiedName)
                value = valueEntry.Value;
            else
                value = valueEntry.Value.ToObject(Type.GetType(valueEntry.TypeName));

            if (valueEntry.ExpireTime != null && valueEntry.ExpireType == ExpireType.Relative)
                SetKeyExpire(key, valueEntry.ExpireTime.Value);

            return value;
        }

        public T GetCache<T>(string key) where T : class
        {
            return (T)GetCache(key);
        }


        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="expire">时间间隔</param>
        public void SetKeyExpire(string key, TimeSpan expire)
        {
            _db.KeyExpire(key, expire);
        }

        public void RemoveCache(string key)
        {
            _db.KeyDelete(key);
        }

        public void SetCache(string key, object value)
        {
            _SetCache(key, value, null, null);
        }

        public void SetCache(string key, object value, TimeSpan timeout)
        {
            _SetCache(key, value, timeout, ExpireType.Absolute);
        }

        public void SetCache(string key, object value, TimeSpan timeout, ExpireType expireType)
        {
            _SetCache(key, value, timeout, expireType);
        }

        private void _SetCache(string key, object value, TimeSpan? timeout, ExpireType? expireType)
        {
            string jsonStr = string.Empty;
            if (value is string)
                jsonStr = value as string;
            else
                jsonStr = value.ToJson();

            ValueInfoEntry entry = new ValueInfoEntry
            {
                Value = jsonStr,
                TypeName = value.GetType().AssemblyQualifiedName,
                ExpireTime = timeout,
                ExpireType = expireType
            };

            string theValue = entry.ToJson();
            if (timeout == null)
                _db.StringSet(key, theValue);
            else
                _db.StringSet(key, theValue, timeout);
        }

        public (bool lockSuccess, bool actionSuccess, Exception ex) UseLock(string key, TimeSpan expiry, Action action)
        {
            if (_db.LockTake(key, key, expiry))
            {
                try
                {
                    action?.Invoke();

                    return (true, true, null);
                }
                catch (Exception ex)
                {
                    return (true, false, ex);
                }
                finally
                {
                    _db.LockRelease(key, key);
                }
            }
            else
            {
                return (false, false, null);
            }
        }

        public (bool lockSuccess, bool actionSuccess, Exception ex) UseLock(string key, TimeSpan expiry, TimeSpan retryTnterval, int retryCount, Action action)
        {
            for (int i = 0; i < retryCount; i++)
            {
                var res = UseLock(key, expiry, action);
                //获取锁失败则重试
                if (!res.lockSuccess)
                {
                    Thread.Sleep(retryTnterval);
                    continue;
                }
                //业务执行成功或异常都直接返回,不重试
                else
                    return res;
            }

            return (false, false, null);
        }

        public (bool lockSuccess, bool actionSuccess, Exception ex) UseLock(string key, TimeSpan expiry, TimeSpan retryTnterval, TimeSpan retryTime, Action action)
        {
            DateTime endTime = DateTime.Now + retryTime;
            while (true)
            {
                if (DateTime.Now < endTime)
                {
                    var res = UseLock(key, expiry, action);
                    //获取锁失败则重试
                    if (!res.lockSuccess)
                    {
                        Thread.Sleep(retryTnterval);
                        continue;
                    }
                    //业务执行成功或异常都直接返回,不重试
                    else
                        return res;
                }
                else
                    return (false, false, null);
            }
        }
    }
}
