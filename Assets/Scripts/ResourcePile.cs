using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A subclass of Building that produce resource at a constant rate.
/// </summary>
public class ResourcePile : Building
{
    public ResourceItem Item;

    private float m_ProductionSpeed = 0.5f; // add new private backing field.
    public float ProductionSpeed // remove value from public property & add a manual get & set functions.
    {
        get { return m_ProductionSpeed; }
        set { 
             // will check here that value which is set in this variable is negative or not, hence restrciting negative numbers through customized getters & setters. 
             if(value < 0.0f)
                Debug.Log("You can't set a negative production speed");
             else
                m_ProductionSpeed = value; 
            }
    }
     

    private float m_CurrentProduction = 0.0f;

    private void Update()
    {
        if (m_CurrentProduction > 1.0f)
        {
            int amountToAdd = Mathf.FloorToInt(m_CurrentProduction);
            int leftOver = AddItem(Item.Id, amountToAdd);

            m_CurrentProduction = m_CurrentProduction - amountToAdd + leftOver;
        }
        
        if (m_CurrentProduction < 1.0f)
        {
            m_CurrentProduction += m_ProductionSpeed * Time.deltaTime;
        }
    }

    public override string GetData()
    {
        return $"Producing at the speed of {m_ProductionSpeed}/s";
        
    }
    
    
}
