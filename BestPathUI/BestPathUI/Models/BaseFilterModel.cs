﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Models.Interfaces;

namespace Models
{
    public abstract class BaseFilterModel : IBaseFilterModel
    {
        public string Getfilter()
        {
            string result = "?";

            foreach (var prop in this.GetType().GetProperties())
            {
                var valore = prop.GetValue(this);
                if (valore != null)
                {
                    if (valore is IEnumerable)
                    {
                        if (prop.Name.EndsWith("ListInt"))
                        {
                            foreach (var item in ((List<int>)valore))
                            {
                                if (result != "?")
                                    result += "&" + prop.Name.Replace("ListInt", "") + "=" + item;
                                else
                                    result += prop.Name.Replace("ListInt", "") + "=" + item;
                            }
                        }
                    }
                    else if (valore.ToString().Trim().Length > 0)
                        if (prop.Name.ToLower().EndsWith("id") && valore.ToString() != "0")
                            if (result != "?")
                                result += "&" + prop.Name + "=" + valore;
                            else
                                result += prop.Name + "=" + valore;
                }
            }
            return result;
        }

    }
}