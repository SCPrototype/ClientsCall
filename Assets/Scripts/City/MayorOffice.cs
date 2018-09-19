using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MayorOffice : FunctionBuilding
{

    private const int _cost = 0; //And 11 happy houses
    private const string _description = "This is your home, you make important decisions from this building.\nIt doesn't really do much nontheless";
    public GameObject _moveAbleHat;

    public MayorOffice()
    {

    }

    void Awake()
    {
        base.Initialize(_cost, _description);
    }

    public MayorOffice Initialize()
    {
        base.Initialize(_cost, _description);
        return this;
    }

    public override void DoAction()
    {
        //Isn't actually a function/collection or production building.
    }

    public void UpdateHatPosition(float pBudget)
    {
        if (_moveAbleHat != null)
        {
            float percentage = pBudget / Glob.BudgetCap;
            percentage = Mathf.Clamp(percentage, 0f, 1f);
            float totalHeight = _moveAbleHat.transform.lossyScale.y;
            _moveAbleHat.transform.localPosition = new Vector3(0, (totalHeight * percentage) * 3.5f, 1.7f);
        }
    }
}