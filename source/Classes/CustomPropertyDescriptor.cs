﻿// Copyright (c) 2012, 2014, Oracle and/or its affiliates. All rights reserved.
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License as
// published by the Free Software Foundation; version 2 of the
// License.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA
// 02110-1301  USA

using System;
using System.ComponentModel;

namespace MySQL.ForExcel.Classes
{
  /// <summary>
  /// Provides an abstraction of a custom property on a class.
  /// </summary>
  internal class CustomPropertyDescriptor : PropertyDescriptor
  {
    /// <summary>
    /// A single property that can be displayed in a property editor.
    /// </summary>
    private readonly CustomProperty _property;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomPropertyDescriptor"/> class.
    /// </summary>
    /// <param name="myProperty">A single property that can be displayed in a property editor.</param>
    /// <param name="attrs">Property attributes.</param>
    public CustomPropertyDescriptor(ref CustomProperty myProperty, Attribute[] attrs)
      : base(myProperty.Name, attrs)
    {
      _property = myProperty;
    }

    #region PropertyDescriptor specific

    /// <summary>
    /// Gets the category name the property belongs to.
    /// </summary>
    public override string Category
    {
      get
      {
        return string.Empty;
      }
    }

    public override Type ComponentType
    {
      get
      {
        return null;
      }
    }

    /// <summary>
    /// Gets the property description.
    /// </summary>
    public override string Description
    {
      get
      {
        return _property.Description;
      }
    }

    /// <summary>
    /// Gets the name of the property.
    /// </summary>
    public override string DisplayName
    {
      get
      {
        return _property.Name;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the property is read only.
    /// </summary>
    public override bool IsReadOnly
    {
      get
      {
        return _property.ReadOnly;
      }
    }

    /// <summary>
    /// Gets the data type of the property.
    /// </summary>
    public override Type PropertyType
    {
      get
      {
        return _property.Value.GetType();
      }
    }

    /// <summary>
    /// Gets a value indicating whether the property value can be reset.
    /// </summary>
    /// <param name="component"></param>
    /// <returns><c>true</c> if the value can be reset, <c>false</c> otherwise.</returns>
    public override bool CanResetValue(object component)
    {
      return false;
    }

    /// <summary>
    /// Gets the property value.
    /// </summary>
    /// <param name="component"></param>
    /// <returns>The property value.</returns>
    public override object GetValue(object component)
    {
      return _property.Value;
    }

    /// <summary>
    /// Resets the property value.
    /// </summary>
    /// <param name="component"></param>
    public override void ResetValue(object component)
    {
      //// Have to implement
    }

    /// <summary>
    /// Sets the property value to the given one.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="value">The new property value.</param>
    public override void SetValue(object component, object value)
    {
      _property.Value = value;
    }

    /// <summary>
    /// Indicates whether the property value is serializable.
    /// </summary>
    /// <param name="component"></param>
    /// <returns><c>true</c> if the property value is serializable, <c>false</c> otherwise.</returns>
    public override bool ShouldSerializeValue(object component)
    {
      return false;
    }

    #endregion PropertyDescriptor specific
  }
}
