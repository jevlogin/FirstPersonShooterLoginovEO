using System.Collections.Generic;
using UnityEngine;

namespace JevLogin
{
    public class BotController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private readonly int _countBot = 5;
        private readonly List<Bot> _botList = new List<Bot>();

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }
            for (var i = 0; i < _botList.Count; i++)
            {
                _botList[i].Execute();
            }
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            for (var index = 0; index < _countBot; index++)
            {
                var tempBot = UnityEngine.Object.Instantiate(ServiceLocatorMonoBehaviour.GetService<Reference>().Bot,
                    Patrol.GenericPoint(ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform),
                    Quaternion.identity);

                tempBot.Agent.avoidancePriority = index;
                tempBot.Target = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform;

                //todo разных противников
                AddBotToList(tempBot);
            }
        }

        private void AddBotToList(Bot tempBot)
        {
            if (!_botList.Contains(tempBot))
            {
                _botList.Add(tempBot);
                tempBot.OnDieChange += RemoveBotToList;
            }
        }

        private void RemoveBotToList(Bot bot)
        {
            if (!_botList.Contains(bot)) return;

            bot.OnDieChange -= RemoveBotToList;
            _botList.Remove(bot);
        }

        #endregion
    }
}
