﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySQL.ExcelAddIn.Properties;

namespace MySQL.ExcelAddIn.Controls
{
  public partial class SearchEdit : UserControl
  {
    private bool isEmpty;
    private int width;

    public SearchEdit()
    {
      InitializeComponent();
      DoubleBuffered = true;
      width = Resources.ExcelAddinFilter.Width;
      innerText_Leave(null, EventArgs.Empty);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      Image i = Resources.ExcelAddinFilter;
      int space = width * 3 / 2;
      e.Graphics.DrawImage(i, (space-width)/2, (Height - i.Height)/2);
    }

    private void innerText_Leave(object sender, EventArgs e)
    {
      if (innerText.Text.Trim().Length == 0)
      {
        innerText.Text = "Filter Schema Objects";
        innerText.ForeColor = Color.Silver;
        isEmpty = true;
      }
    }

    private void innerText_Enter(object sender, EventArgs e)
    {
      if (isEmpty)
      {
        innerText.Text = String.Empty;
        isEmpty = false;
        innerText.ForeColor = SystemColors.WindowText;
      }
    }

    private void SearchEdit_Resize(object sender, EventArgs e)
    {
      innerText.SetBounds(width*3/2 , (Height - innerText.Height)/2, Size.Width - width, innerText.Height);
    }
  }
}