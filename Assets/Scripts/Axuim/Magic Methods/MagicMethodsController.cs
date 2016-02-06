using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Axuim.MagicMethods
{
    #region MagicMethodsController

    public class MagicMethodsController : MonoBehaviour
    {
        #region Private Properties

        private static MagicMethodsController _instance;
        private static MagicMethodsController Instance
        {
            get
            {
                if (_instance == null)
                {
                    var gameObject = new GameObject("Magic Methods Controller");
                    gameObject.AddComponent<MagicMethodsController>();
                }

                return _instance;
            }
        }

        private List<IMagicUpdate> _updaters = new List<IMagicUpdate>();
        private List<IMagicFixedUpdate> _fixedUpdaters = new List<IMagicFixedUpdate>();
        private List<IMagicLateUpdate> _lateUpdaters = new List<IMagicLateUpdate>();

        #endregion
        
        #region MonoBehaviour

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;

                GameObject.DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                GameObject.Destroy(this);
            }
        }

        private void Update()
        {
            foreach (var updater in _updaters.ToArray())
            {
                if (updater != null)
                {
                    updater.MagicUpdate();
                }
            }
        }

        private void FixedUpdate()
        {
            foreach (var fixedUpdater in _fixedUpdaters.ToArray())
            {
                if (fixedUpdater != null)
                {
                    fixedUpdater.MagicFixedUpdate();
                }
            }
        }

        private void LateUpdate()
        {
            foreach (var lateUpdater in _lateUpdaters.ToArray())
            {
                if (lateUpdater != null)
                {
                    lateUpdater.MagicLateUpdate();
                }
            }
        }

        #endregion

        #region Public Methods

        public static void AddUpdater(IMagicUpdate updater)
        {
            MagicMethodsController.Instance.InternalAddUpdater(updater);
        }

        public static void AddFixedUpdater(IMagicFixedUpdate updater)
        {
            MagicMethodsController.Instance.InternalAddFixedUpdater(updater);
        }

        public static void AddLateUpdater(IMagicLateUpdate updater)
        {
            MagicMethodsController.Instance.InternalAddLateUpdater(updater);
        }

        public static bool RemoveUpdater(IMagicUpdate updater)
        {
            return MagicMethodsController.Instance.InternalRemoveUpdater(updater);
        }

        public static bool RemoveFixedUpdater(IMagicFixedUpdate updater)
        {
            return MagicMethodsController.Instance.InternalRemoveFixedUpdater(updater);
        }

        public static bool RemoveLateUpdater(IMagicLateUpdate updater)
        {
            return MagicMethodsController.Instance.InternalRemoveLateUpdater(updater);
        }

        #endregion

        #region Private Methods

        private void InternalAddUpdater(IMagicUpdate updater)
        {
            _updaters.Add(updater);
        }

        private void InternalAddFixedUpdater(IMagicFixedUpdate updater)
        {
            _fixedUpdaters.Add(updater);
        }

        private void InternalAddLateUpdater(IMagicLateUpdate updater)
        {
            _lateUpdaters.Add(updater);
        }

        public bool InternalRemoveUpdater(IMagicUpdate updater)
        {
            bool result = false;

            if (_instance != null)
            {
                result = _updaters.Remove(updater);
            }

            return result;
        }

        public bool InternalRemoveFixedUpdater(IMagicFixedUpdate updater)
        {
            bool result = false;

            if (_instance != null)
            {
                result = _fixedUpdaters.Remove(updater);
            }

            return result;
        }

        public bool InternalRemoveLateUpdater(IMagicLateUpdate updater)
        {
            bool result = false;

            if (_instance != null)
            {
                result = _lateUpdaters.Remove(updater);
            }

            return result;
        }

        #endregion
    }

    #endregion

    #region Interfaces

    public interface IMagicUpdate
    {
        void MagicUpdate();
    }

    public interface IMagicFixedUpdate
    {
        void MagicFixedUpdate();
    }

    public interface IMagicLateUpdate
    {
        void MagicLateUpdate();
    }

    #endregion
}