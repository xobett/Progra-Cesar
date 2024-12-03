using System;
using UnityEngine;



/// <summary>
/// 
/// NECESITAMOS
/// Tener paginas en el inventario, cada pagina se conforma por 8 items como maximo
/// Conforme se agregan items al inventario, se deben de ir desbloqueando paginas
///
/// 
/// </summary>
/// 

namespace Profe
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryCanvas;
        [SerializeField] private GameObject uiItemPrefab;
        [SerializeField] private GameObject displayArea;
        [SerializeField] private Page[] pages = new Page[3]; // 24 items

        public int actualPage = 0;
        private int maxItemsPerPage = 8;

        private InventoryHandler inventoryRef;

        public bool inventoryOpened = false;

        private void Awake()
        {          
            inventoryRef = FindAnyObjectByType<InventoryHandler>();

            for(int i= 0; i<pages.Length; i++)
            {
                pages[i].items = new GameObject[maxItemsPerPage];
                pages[i].itemsDeployed = 0;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I)) // Abrir inventario
            {
                OpenInventory();
            }
        }

        private void OpenInventory()
        {

            inventoryOpened = !inventoryOpened;
            inventoryCanvas.SetActive(inventoryOpened);

           if(inventoryRef.inventory.Count <= 0) // Revisa si hay cosas en el inventario
            {
                // Si no hay nada, aqui termina
                return;
            }
            else
            {
                //   i = el item en el que voy en mi pagina actual
                for(int i = pages[actualPage].itemsDeployed; i < inventoryRef.inventory.Count; i++) // Tenemos 1 item
                {               
                    GameObject item = Instantiate(uiItemPrefab); // Creo un item en el canvas
                    item.transform.SetParent(displayArea.transform); // Lo emparento al area del libro/display/area util
                    item.transform.localScale = Vector3.one; // le pongo la escala en 1 por que a veces sale de diferente tama�o no se porque
                    item.GetComponent<ItemUI>().SetItemInfo(inventoryRef.inventory[i]); // le asigno la informacion
                    pages[actualPage].items[pages[actualPage].itemsDeployed] = item; // guardo el item en la posicion correspondiente de la pagina actual
                    // pages[actualPage].items estoy accediendo a mi arreglo de items en mi pagina actual
                    // items[pages[actualPage].itemsDeployed] estoy accediendo al item que sigue, es decir, donde voy a guardar mi item
                    pages[actualPage].itemsDeployed++; // 8

                    if (pages[actualPage].itemsDeployed >= maxItemsPerPage) // Si ya alcance mi capacidad maxima de items en mi pagina actual
                    {
                        actualPage++; // paso a la siguiente pagina
                    }
                }

                HideAllItems();
                ShowItems(actualPage);

            }
        }

       

        [ContextMenu("Show Items in Page")]
        private void ShowItems()
        {
            for (int i = 0; i < pages[actualPage].itemsDeployed; i++)
            {
                pages[actualPage].items[i].SetActive(true);
            }
        }

        [ContextMenu("Hide Items in Page")]
        private void HideItems()
        {
            for (int i = 0; i < pages[actualPage].itemsDeployed; i++)
            {
                pages[actualPage].items[i].SetActive(false);
            }
        }

        // Este metodo ahorita me lo guardo para cuando tenga el boton de cambiar pagina
        private void ShowItems(int page)
        {
            for (int i = 0; i < pages[page].itemsDeployed; i++)
            {
                pages[page].items[i].SetActive(true);
            }
        }

        // Este metodo ahorita me lo guardo para cuando tenga el boton de cambiar pagina
        private void HideItems(int page)
        {
            for (int i = 0; i < pages[page].itemsDeployed; i++)
            {
                pages[page].items[i].SetActive(false);
            }
        }

        [ContextMenu("Hide All Items")]
        private void HideAllItems()
        {
            for(int page = 0; page <= actualPage; page++) // Este for recorre las paginas
            {
                Debug.Log(page);
                for(int item = 0; item < pages[page].itemsDeployed; item++)
                {
                    Debug.Log(item);
                    pages[page].items[item].SetActive(false);
                }
                Debug.Log("Siguiente pagina");
            }
        }
        
    }

    [Serializable]
    public struct Page
    {
        public int itemsDeployed;
        public GameObject[] items; // en este arreglo me guarda los 8 items que pertenecen a esa pagina
    }

}



