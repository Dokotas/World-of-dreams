using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

namespace WorldOfDreams
{
    public class ResourceService : IInitializable, IDisposable
    {
        public ReadOnlyReactiveProperty<ResourceData> HpStream { get; private set; }
        public ReadOnlyReactiveProperty<ResourceData> ManaStream { get; private set; }

        private readonly BehaviorSubject<ResourceData> _hpSubject;
        private readonly BehaviorSubject<ResourceData> _manaSubject;

        private ResourceData _hp;
        private ResourceData _mana;

        private CancellationTokenSource _cts;

        public ResourceService()
        {
            _hp = new ResourceData(0, 100);
            _mana = new ResourceData(0, 100);

            _hpSubject = new BehaviorSubject<ResourceData>(_hp);
            _manaSubject = new BehaviorSubject<ResourceData>(_mana);

            HpStream = _hpSubject.ToReadOnlyReactiveProperty();
            ManaStream = _manaSubject.ToReadOnlyReactiveProperty();
        }

        public void Initialize()
        {
            _cts = new CancellationTokenSource();
            GrowthLoop(_cts.Token).Forget();
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _hpSubject.Dispose();
            _manaSubject.Dispose();
        }

        private async UniTask GrowthLoop(CancellationToken ct)
        {
            int step = 0;

            try
            {
                while (!ct.IsCancellationRequested)
                {
                    SetHp(step*10);
                    SetMana(100-step * 10);

                    step++;
                    if (step >= 10)
                    {
                        step = 0;
                    }
                    await UniTask.WaitForSeconds(1f, cancellationToken: ct);
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Growth loop is ended from cancel token");
            }
        }

        public void SetHp(float value)
        {
            _hp.Current = Mathf.Clamp(value, 0, _hp.Max);
            _hpSubject.OnNext(_hp);
        }

        public void SetMana(float value)
        {
            _mana.Current = Mathf.Clamp(value, 0, _mana.Max);
            _manaSubject.OnNext(_mana);
        }
    }
}