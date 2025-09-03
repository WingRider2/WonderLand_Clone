using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
public interface ILoadData
{
    void LoadEffectData(Newtonsoft.Json.Linq.JObject effectValues); // JSON 파라미터 주입
}
