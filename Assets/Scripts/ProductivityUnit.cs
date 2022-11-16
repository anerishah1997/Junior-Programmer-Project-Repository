using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductivityUnit : Unit
{
    //new variables 
    private ResourcePile m_CurrentPile = null;
    public float productivityMultiplier = 2; // to multiply the resource's production rate when this boy productivity unit goes towards that resource pile.

    protected override void BuildingInRange()
    {
         if(m_CurrentPile == null)
         {
            ResourcePile pile = m_Target as ResourcePile; // 'as ResourcePile' sets the pile variable to m_Target only whent the target is a ResourcePile & not any other target like Base.

            if (pile != null)
            {
                Debug.Log("ResoourcePile In Range");
                m_CurrentPile = pile; // if target is resourcePile then assign it to the currentPile
                m_CurrentPile.ProductionSpeed -= productivityMultiplier;  // and its production speed is doubled.
            }
         }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
