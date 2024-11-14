using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.Serialization;
using UnityEngine;

public struct DataPlayer
{
}

public class WrapperData : IInitialize
{
    public DataPlayer Client => _client;
    private DataPlayer _client;

    
    public void OnInitialize()
    {
        var data = PlayerPrefs.GetString(nameof(DataPlayer), string.Empty);
        _client = !string.IsNullOrEmpty(data)
            ? JsonConvert.DeserializeObject<DataPlayer>(data)
            : new DataPlayer()
            {
                
            };
    }   

    private void SaveOnClient()
    {
        PlayerPrefs.SetString(nameof(DataPlayer), JsonConvert.SerializeObject(_client));
    }
}