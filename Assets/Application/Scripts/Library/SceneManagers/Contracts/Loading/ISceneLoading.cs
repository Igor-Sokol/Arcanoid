using UnityEngine;

namespace Application.Scripts.Library.SceneManagers.Contracts.Loading
{
    public interface ISceneLoading
    {
        YieldInstruction Enable();
        YieldInstruction Disable();
    }
}