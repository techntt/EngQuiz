using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABIPlugins;
using UnityEngine.UI;

namespace ABIPlugins {
	public class PopupManagerAbi : MonoBehaviour {
		public Canvas canvas;
		public bool usingDefaultTransparent = true;
		public BasePopup[] prefabs;
		public Image transparent;
		private Transform mTransparentTrans;
		public Stack<BasePopup> popupStacks = new Stack<BasePopup>();
		public Transform parent;
		private int defaultSortingOrder;

		private static PopupManagerAbi mInstance;

		public static PopupManagerAbi Instance {
			get {
				if (mInstance == null) {
					mInstance = FindObjectOfType<PopupManagerAbi>();
					if (mInstance == null) {
						LoadResource<PopupManagerAbi>("PopupManager");
					} 
				}

				return mInstance;
			}
		}

		void Awake () {
			mInstance = this;
			mTransparentTrans = transparent.transform;
			defaultSortingOrder = canvas.sortingOrder;
			DontDestroyOnLoad(gameObject);
		}

		public static T CreateNewInstance<T> () {
			T result = Instance.CheckInstancePopupPrebab<T>();
			return result;
		}

		public T CheckInstancePopupPrebab<T> () {
			System.Type type = typeof(T);
			GameObject go = null;
			for (int i = 0; i < prefabs.Length; i++) {
				if (IsOfType<T>(prefabs[i])) {
					go = (GameObject)Instantiate(prefabs[i].gameObject, parent);
					break;
				}
			}
			T result = go.GetComponent<T>();
			return result;
		}

		private bool IsOfType<T> (object value) {
			return value is T;
		}

		public void ChangeTransparentOrder (Transform topPopupTransform, bool active) {
			if (active) {
				mTransparentTrans.SetSiblingIndex(topPopupTransform.GetSiblingIndex() - 1);
				transparent.gameObject.SetActive(true && usingDefaultTransparent);
			} else {
				if (parent.childCount > 2) {
					mTransparentTrans.SetSiblingIndex(topPopupTransform.GetSiblingIndex() - 2);
				} else {
					transparent.gameObject.SetActive(false);
				}
			}
		}


		public PopupManagerAbi Preload () {
			return mInstance;
		}

		public bool SequenceHidePopup () {
			if (popupStacks.Count > 0)
				popupStacks.Peek().Hide();
			else
				transparent.gameObject.SetActive(false);
			return (popupStacks.Count > 0);
		}

		public void CloseAllPopup () {
			for (int i = 0; i < popupStacks.Count; i++) {
				popupStacks.Peek().Hide();
			}
			transparent.gameObject.SetActive(false);
		}

		public static T LoadResource<T> (string name) {
			GameObject go = (GameObject)GameObject.Instantiate(Resources.Load(name));
			go.name = string.Format("[{0}]", name);
			DontDestroyOnLoad(go);
			return go.GetComponent<T>();
		}

		public void SetSortingOrder (int order) {
			canvas.sortingOrder = order;
		}

		public void ResetOrder () {
			canvas.sortingOrder = defaultSortingOrder;
		}
	}
}