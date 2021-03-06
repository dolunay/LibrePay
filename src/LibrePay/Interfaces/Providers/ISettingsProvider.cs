﻿using System.Threading.Tasks;

namespace LibrePay.Interfaces.Providers
{
    public interface ISettingsProvider
    {
        Task<T> GetValueAsync<T>(string key);

        Task<T> GetSecureValueAsync<T>(string key);

        Task SetValueAsync<T>(string key, T value);

        Task SetSecureValueAsync<T>(string key, T value);
    }
}
