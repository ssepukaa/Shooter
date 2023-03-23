

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Infrastructure {

    // Можно расширить, чтобы удалялись неиспользуемые объекты , если их было создано слишком много
    // Надо добавить в остальные места где есть Instantiate
    public class PoolMono<T> where T : MonoBehaviour {
        public T prefab { get; }
        public bool autoExpand { get; set; }
        public Transform container { get; }
        private List<T> pool;

        public PoolMono(T prefab, int count) {
            this.prefab = prefab;
            this.container = null;
            this.autoExpand = true;
            this.CreatePool(count);
        }

        public PoolMono(T prefab, int count, Transform container) {
            this.prefab = prefab;
            this.container = container;
            this.autoExpand = true;
            this.CreatePool(count);
        }

        private void CreatePool(int count) {
            this.pool = new List<T>();
            for (int i = 0; i < count; i++) {
                this.CreateObject();
            }
        }

        private T CreateObject(bool isActiveByDefault = false) {
            var createdObject = Object.Instantiate(this.prefab, this.container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            this.pool.Add(createdObject);
            return createdObject;
        }

        public bool HasFreeElement(out T element) {
            foreach (var mono in pool) {
                if (!mono.gameObject.activeInHierarchy) {
                    element = mono;
                    mono.gameObject.SetActive(true);
                    return true;
                }
            }
            element = null;
            return false;
        }

        public T GetFreeElement() {
            if (this.HasFreeElement(out var element))
                return element;
            if (this.autoExpand)
                return this.CreateObject(true);
            throw new Exception($"There is no free elements in pool of type{typeof(T)} ");
        }
        // Можно добавить методы по поиску всех элементов в пуле для установки каких-то параметров и прочие методы
    }

    // Пример использования
    //__________________________________________________________________________________
    // Добавить к объекту, который будет появляться и вызываться при окончании жизни объекта или его попадании(пуля)
    public class ExampleObject : MonoBehaviour {
        [SerializeField] private float lifeTime = 0.5f;

        private void OnEnable() {
            this.StartCoroutine("LifeRoutine");
        }

        private void OnDisable() {
            this.StopCoroutine("LifeRoutine");
        }

        private IEnumerator LifeRoutine() {
            yield return new WaitForSecondsRealtime(this.lifeTime);
            this.Deactivate();
        }

        private void Deactivate() {
            this.gameObject.SetActive(false);
        }

    }
    //______________________________________________________________________
    // Добавить к создаваемому объекту/менеджеру , например WeaponManager, SVFXManager
    public class ExamplePool : MonoBehaviour {
        [Header("Емкость пула")]
        [SerializeField] private int poolCount = 3;
        [Header("Авторасширение пула")]
        [SerializeField] private bool autoExpand = true;
        [Header("Ссылка на префаб: может быть любой кастомный тип объекта")]
        [SerializeField] private ExampleObject objectPrefab;

        private PoolMono<ExampleObject> pool; //может быть любой кастомный тип объекта

        private void Start() {
            this.pool = new PoolMono<ExampleObject>(this.objectPrefab, this.poolCount, this.transform);
            this.pool.autoExpand = this.autoExpand;
        }

        private void CreateObject() {
            var newObject = this.pool.GetFreeElement();
            // передать Transform 
            // newObject.transform.position = position;

        }
    }
}
