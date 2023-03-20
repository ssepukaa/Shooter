using Assets.Scripts.UI.WeaponUI.UIWidgetWeaponBarFolder;
using Assets.Scripts.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.WeaponUI {

    public class UIWidgetWeaponBar : MonoBehaviour {
        [SerializeField] private GameObject[] _weaponItemArray = new GameObject[5];
        private Weapon _weapon;
        private int _indexReference = -1;

        private void Start() {
            
           
        }

        public void InitStart(Weapon weapon) {
            _weapon = weapon;
            EnableStateNone();

        }
        public GameObject GetReferenceItem() {
            _indexReference++;
            
            return _weaponItemArray[_indexReference];
        }

        public void SetImageWeapon(GameObject weaponItem, Sprite sprite) {

            var weaponImage = weaponItem.GetComponent<UnityEngine.UI.Image>();
            weaponImage.sprite = sprite;
            weaponImage.enabled = true;
        }

        public void EnableStateNone() {
            foreach (var weapon in _weaponItemArray) {
                weapon.GetComponent<UnityEngine.UI.Image>().enabled = false;
                weapon.GetComponent<Button>().enabled = false;
                weapon.GetComponentInChildren<NonItemImage>().GetComponent<UnityEngine.UI.Image>().enabled = true;
                weapon.GetComponentInChildren<RectChangeImage>().GetComponent<UnityEngine.UI.Image>().enabled = false;
                weapon.GetComponentInChildren<ReloadImage>().GetComponent<UnityEngine.UI.Image>().enabled = false;
                


            }
           
        }

        public void EnableIdleState(GameObject weapon) {
            weapon.GetComponent<UnityEngine.UI.Image>().enabled = true;
            weapon.GetComponent<Button>().enabled = true;
            weapon.GetComponentInChildren<NonItemImage>().GetComponent<UnityEngine.UI.Image>().enabled = false;
            weapon.GetComponentInChildren<RectChangeImage>().GetComponent<UnityEngine.UI.Image>().enabled = false;
            weapon.GetComponentInChildren<ReloadImage>().GetComponent<UnityEngine.UI.Image>().enabled = false;

        }

        public void EnableReloadState(GameObject weapon) {
            weapon.GetComponentInChildren<ReloadImage>().GetComponent<UnityEngine.UI.Image>().enabled = true;

        }

        public void DisableReloadState(GameObject weapon) {
            DisableHolderState();
            weapon.GetComponentInChildren<ReloadImage>().GetComponent<UnityEngine.UI.Image>().enabled = false;

        }

        public void EnableHolderState(GameObject weapon) {
            weapon.GetComponentInChildren<RectChangeImage>().GetComponent<UnityEngine.UI.Image>().enabled = true;
        }
        public void DisableHolderState() {
            foreach (var weapon in _weaponItemArray) {
                weapon.GetComponentInChildren<RectChangeImage>().GetComponent<UnityEngine.UI.Image>().enabled = false;

            }
        }

        public void Button1() {
            DisableHolderState();
            EnableHolderState(_weaponItemArray[0]);
            _weapon.ActivateWeapon(1);
        }public void Button2() {
            DisableHolderState();
            EnableHolderState(_weaponItemArray[1]);
            _weapon.ActivateWeapon(2);
        }public void Button3() {
            DisableHolderState();
            EnableHolderState(_weaponItemArray[2]);
            _weapon.ActivateWeapon(3);
        }public void Button4() {
            DisableHolderState();
            EnableHolderState(_weaponItemArray[3]);
            _weapon.ActivateWeapon(4);
        }public void Button5() {
            DisableHolderState();
            EnableHolderState(_weaponItemArray[4]);
            _weapon.ActivateWeapon(5);
        }

    }
}
