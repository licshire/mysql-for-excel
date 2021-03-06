﻿// Copyright (c) 2012, 2015, Oracle and/or its affiliates. All rights reserved.
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

namespace MySQL.ForExcel.Forms
{
  partial class ExportAdvancedOptionsDialog
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing"><c>true</c> if managed resources should be disposed; otherwise, <c>false</c>.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportAdvancedOptionsDialog));
      this.DialogAcceptButton = new System.Windows.Forms.Button();
      this.DialogCancelButton = new System.Windows.Forms.Button();
      this.UseFormattedValuesCheckBox = new System.Windows.Forms.CheckBox();
      this.FieldDataOptionsLabel = new System.Windows.Forms.Label();
      this.AutoAllowEmptyNonIndexColumnsCheckBox = new System.Windows.Forms.CheckBox();
      this.AutoIndexIntColumnsCheckBox = new System.Windows.Forms.CheckBox();
      this.AddBufferToVarCharCheckBox = new System.Windows.Forms.CheckBox();
      this.DetectDatatypeCheckBox = new System.Windows.Forms.CheckBox();
      this.ColumnDatatypeOptionsLabel = new System.Windows.Forms.Label();
      this.AdvancedExportOptionsLabel = new System.Windows.Forms.Label();
      this.PreviewRowsQuantity1Label = new System.Windows.Forms.Label();
      this.PreviewRowsQuantity2Label = new System.Windows.Forms.Label();
      this.PreviewRowsQuantityNumericUpDown = new System.Windows.Forms.NumericUpDown();
      this.ColumnOptionsLostWarningLabel = new System.Windows.Forms.Label();
      this.ColumnOptionsLostWarningPictureBox = new System.Windows.Forms.PictureBox();
      this.CreateTableIndexesLastCheckBox = new System.Windows.Forms.CheckBox();
      this.SqlQueriesLabel = new System.Windows.Forms.Label();
      this.HelpToolTip = new System.Windows.Forms.ToolTip(this.components);
      this.ShowAllDataTypesCheckBox = new System.Windows.Forms.CheckBox();
      this.GenerateMultipleInsertsCheckBox = new System.Windows.Forms.CheckBox();
      this.ResetToDefaultsButton = new System.Windows.Forms.Button();
      this.ContentAreaPanel.SuspendLayout();
      this.CommandAreaPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.PreviewRowsQuantityNumericUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ColumnOptionsLostWarningPictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // FootnoteAreaPanel
      // 
      this.FootnoteAreaPanel.Location = new System.Drawing.Point(0, 292);
      this.FootnoteAreaPanel.Size = new System.Drawing.Size(634, 0);
      // 
      // ContentAreaPanel
      // 
      this.ContentAreaPanel.Controls.Add(this.GenerateMultipleInsertsCheckBox);
      this.ContentAreaPanel.Controls.Add(this.ShowAllDataTypesCheckBox);
      this.ContentAreaPanel.Controls.Add(this.CreateTableIndexesLastCheckBox);
      this.ContentAreaPanel.Controls.Add(this.SqlQueriesLabel);
      this.ContentAreaPanel.Controls.Add(this.ColumnOptionsLostWarningLabel);
      this.ContentAreaPanel.Controls.Add(this.PreviewRowsQuantityNumericUpDown);
      this.ContentAreaPanel.Controls.Add(this.ColumnOptionsLostWarningPictureBox);
      this.ContentAreaPanel.Controls.Add(this.PreviewRowsQuantity2Label);
      this.ContentAreaPanel.Controls.Add(this.PreviewRowsQuantity1Label);
      this.ContentAreaPanel.Controls.Add(this.AdvancedExportOptionsLabel);
      this.ContentAreaPanel.Controls.Add(this.UseFormattedValuesCheckBox);
      this.ContentAreaPanel.Controls.Add(this.FieldDataOptionsLabel);
      this.ContentAreaPanel.Controls.Add(this.AutoAllowEmptyNonIndexColumnsCheckBox);
      this.ContentAreaPanel.Controls.Add(this.AutoIndexIntColumnsCheckBox);
      this.ContentAreaPanel.Controls.Add(this.AddBufferToVarCharCheckBox);
      this.ContentAreaPanel.Controls.Add(this.DetectDatatypeCheckBox);
      this.ContentAreaPanel.Controls.Add(this.ColumnDatatypeOptionsLabel);
      this.ContentAreaPanel.Size = new System.Drawing.Size(584, 476);
      // 
      // CommandAreaPanel
      // 
      this.CommandAreaPanel.Controls.Add(this.ResetToDefaultsButton);
      this.CommandAreaPanel.Controls.Add(this.DialogAcceptButton);
      this.CommandAreaPanel.Controls.Add(this.DialogCancelButton);
      this.CommandAreaPanel.Location = new System.Drawing.Point(0, 431);
      this.CommandAreaPanel.Size = new System.Drawing.Size(584, 45);
      // 
      // DialogAcceptButton
      // 
      this.DialogAcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.DialogAcceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.DialogAcceptButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.DialogAcceptButton.Location = new System.Drawing.Point(416, 11);
      this.DialogAcceptButton.Name = "DialogAcceptButton";
      this.DialogAcceptButton.Size = new System.Drawing.Size(75, 23);
      this.DialogAcceptButton.TabIndex = 1;
      this.DialogAcceptButton.Text = "Accept";
      this.DialogAcceptButton.UseVisualStyleBackColor = true;
      // 
      // DialogCancelButton
      // 
      this.DialogCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.DialogCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.DialogCancelButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.DialogCancelButton.Location = new System.Drawing.Point(497, 11);
      this.DialogCancelButton.Name = "DialogCancelButton";
      this.DialogCancelButton.Size = new System.Drawing.Size(75, 23);
      this.DialogCancelButton.TabIndex = 2;
      this.DialogCancelButton.Text = "Cancel";
      this.DialogCancelButton.UseVisualStyleBackColor = true;
      // 
      // UseFormattedValuesCheckBox
      // 
      this.UseFormattedValuesCheckBox.AutoSize = true;
      this.UseFormattedValuesCheckBox.BackColor = System.Drawing.Color.Transparent;
      this.UseFormattedValuesCheckBox.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.UseFormattedValuesCheckBox.Location = new System.Drawing.Point(53, 273);
      this.UseFormattedValuesCheckBox.Name = "UseFormattedValuesCheckBox";
      this.UseFormattedValuesCheckBox.Size = new System.Drawing.Size(137, 19);
      this.UseFormattedValuesCheckBox.TabIndex = 11;
      this.UseFormattedValuesCheckBox.Text = "Use formatted values";
      this.HelpToolTip.SetToolTip(this.UseFormattedValuesCheckBox, "If checked it treats dates in Excel as such, otherwise it treats them as numbers." +
        "");
      this.UseFormattedValuesCheckBox.UseVisualStyleBackColor = false;
      this.UseFormattedValuesCheckBox.CheckedChanged += new System.EventHandler(this.UseFormattedValuesCheckBox_CheckedChanged);
      // 
      // FieldDataOptionsLabel
      // 
      this.FieldDataOptionsLabel.AutoSize = true;
      this.FieldDataOptionsLabel.BackColor = System.Drawing.Color.Transparent;
      this.FieldDataOptionsLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FieldDataOptionsLabel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.FieldDataOptionsLabel.Location = new System.Drawing.Point(24, 244);
      this.FieldDataOptionsLabel.Name = "FieldDataOptionsLabel";
      this.FieldDataOptionsLabel.Size = new System.Drawing.Size(116, 17);
      this.FieldDataOptionsLabel.TabIndex = 10;
      this.FieldDataOptionsLabel.Text = "Field Data Options";
      // 
      // AutoAllowEmptyNonIndexColumnsCheckBox
      // 
      this.AutoAllowEmptyNonIndexColumnsCheckBox.AutoSize = true;
      this.AutoAllowEmptyNonIndexColumnsCheckBox.BackColor = System.Drawing.Color.Transparent;
      this.AutoAllowEmptyNonIndexColumnsCheckBox.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.AutoAllowEmptyNonIndexColumnsCheckBox.Location = new System.Drawing.Point(53, 185);
      this.AutoAllowEmptyNonIndexColumnsCheckBox.Name = "AutoAllowEmptyNonIndexColumnsCheckBox";
      this.AutoAllowEmptyNonIndexColumnsCheckBox.Size = new System.Drawing.Size(435, 19);
      this.AutoAllowEmptyNonIndexColumnsCheckBox.TabIndex = 8;
      this.AutoAllowEmptyNonIndexColumnsCheckBox.Text = "Automatically check the Allow Empty checkbox for columns without an index";
      this.HelpToolTip.SetToolTip(this.AutoAllowEmptyNonIndexColumnsCheckBox, "When checked the columns without an index are set to allow empty (null) values.");
      this.AutoAllowEmptyNonIndexColumnsCheckBox.UseVisualStyleBackColor = false;
      this.AutoAllowEmptyNonIndexColumnsCheckBox.CheckedChanged += new System.EventHandler(this.AutoAllowEmptyNonIndexColumnsCheckBox_CheckedChanged);
      // 
      // AutoIndexIntColumnsCheckBox
      // 
      this.AutoIndexIntColumnsCheckBox.AutoSize = true;
      this.AutoIndexIntColumnsCheckBox.BackColor = System.Drawing.Color.Transparent;
      this.AutoIndexIntColumnsCheckBox.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.AutoIndexIntColumnsCheckBox.Location = new System.Drawing.Point(53, 160);
      this.AutoIndexIntColumnsCheckBox.Name = "AutoIndexIntColumnsCheckBox";
      this.AutoIndexIntColumnsCheckBox.Size = new System.Drawing.Size(345, 19);
      this.AutoIndexIntColumnsCheckBox.TabIndex = 7;
      this.AutoIndexIntColumnsCheckBox.Text = "Automatically check the Index checkbox for Integer columns";
      this.HelpToolTip.SetToolTip(this.AutoIndexIntColumnsCheckBox, "When checked the columns with a detected data type of Integer are set to have an " +
        "index added to the column.");
      this.AutoIndexIntColumnsCheckBox.UseVisualStyleBackColor = false;
      this.AutoIndexIntColumnsCheckBox.CheckedChanged += new System.EventHandler(this.AutoIndexIntColumnsCheckBox_CheckedChanged);
      // 
      // AddBufferToVarCharCheckBox
      // 
      this.AddBufferToVarCharCheckBox.AutoSize = true;
      this.AddBufferToVarCharCheckBox.BackColor = System.Drawing.Color.Transparent;
      this.AddBufferToVarCharCheckBox.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.AddBufferToVarCharCheckBox.Location = new System.Drawing.Point(73, 135);
      this.AddBufferToVarCharCheckBox.Name = "AddBufferToVarCharCheckBox";
      this.AddBufferToVarCharCheckBox.Size = new System.Drawing.Size(406, 19);
      this.AddBufferToVarCharCheckBox.TabIndex = 6;
      this.AddBufferToVarCharCheckBox.Text = "Add additional buffer to Varchar length (round up to 12, 25, 45, 125, 255)";
      this.AddBufferToVarCharCheckBox.UseVisualStyleBackColor = false;
      // 
      // DetectDatatypeCheckBox
      // 
      this.DetectDatatypeCheckBox.AutoSize = true;
      this.DetectDatatypeCheckBox.BackColor = System.Drawing.Color.Transparent;
      this.DetectDatatypeCheckBox.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.DetectDatatypeCheckBox.Location = new System.Drawing.Point(53, 110);
      this.DetectDatatypeCheckBox.Name = "DetectDatatypeCheckBox";
      this.DetectDatatypeCheckBox.Size = new System.Drawing.Size(416, 19);
      this.DetectDatatypeCheckBox.TabIndex = 5;
      this.DetectDatatypeCheckBox.Text = "Analyze and try to detect correct datatype based on column field contents";
      this.HelpToolTip.SetToolTip(this.DetectDatatypeCheckBox, "When checked the data type on each new column will be automatically detected base" +
        "d on the Excel data values.");
      this.DetectDatatypeCheckBox.UseVisualStyleBackColor = false;
      this.DetectDatatypeCheckBox.CheckedChanged += new System.EventHandler(this.DetectDatatypeCheckBox_CheckedChanged);
      // 
      // ColumnDatatypeOptionsLabel
      // 
      this.ColumnDatatypeOptionsLabel.AutoSize = true;
      this.ColumnDatatypeOptionsLabel.BackColor = System.Drawing.Color.Transparent;
      this.ColumnDatatypeOptionsLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ColumnDatatypeOptionsLabel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.ColumnDatatypeOptionsLabel.Location = new System.Drawing.Point(24, 56);
      this.ColumnDatatypeOptionsLabel.Name = "ColumnDatatypeOptionsLabel";
      this.ColumnDatatypeOptionsLabel.Size = new System.Drawing.Size(158, 17);
      this.ColumnDatatypeOptionsLabel.TabIndex = 1;
      this.ColumnDatatypeOptionsLabel.Text = "Column Datatype Options";
      // 
      // AdvancedExportOptionsLabel
      // 
      this.AdvancedExportOptionsLabel.AutoSize = true;
      this.AdvancedExportOptionsLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.AdvancedExportOptionsLabel.ForeColor = System.Drawing.Color.Navy;
      this.AdvancedExportOptionsLabel.Location = new System.Drawing.Point(17, 17);
      this.AdvancedExportOptionsLabel.Name = "AdvancedExportOptionsLabel";
      this.AdvancedExportOptionsLabel.Size = new System.Drawing.Size(178, 20);
      this.AdvancedExportOptionsLabel.TabIndex = 0;
      this.AdvancedExportOptionsLabel.Text = "Advanced Export Options";
      // 
      // PreviewRowsQuantity1Label
      // 
      this.PreviewRowsQuantity1Label.AutoSize = true;
      this.PreviewRowsQuantity1Label.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.PreviewRowsQuantity1Label.Location = new System.Drawing.Point(50, 85);
      this.PreviewRowsQuantity1Label.Name = "PreviewRowsQuantity1Label";
      this.PreviewRowsQuantity1Label.Size = new System.Drawing.Size(69, 15);
      this.PreviewRowsQuantity1Label.TabIndex = 2;
      this.PreviewRowsQuantity1Label.Text = "Use the first";
      // 
      // PreviewRowsQuantity2Label
      // 
      this.PreviewRowsQuantity2Label.AutoSize = true;
      this.PreviewRowsQuantity2Label.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.PreviewRowsQuantity2Label.Location = new System.Drawing.Point(185, 85);
      this.PreviewRowsQuantity2Label.Name = "PreviewRowsQuantity2Label";
      this.PreviewRowsQuantity2Label.Size = new System.Drawing.Size(275, 15);
      this.PreviewRowsQuantity2Label.TabIndex = 4;
      this.PreviewRowsQuantity2Label.Text = "Excel data rows to preview and calculate datatypes.";
      // 
      // PreviewRowsQuantityNumericUpDown
      // 
      this.PreviewRowsQuantityNumericUpDown.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.PreviewRowsQuantityNumericUpDown.Location = new System.Drawing.Point(127, 83);
      this.PreviewRowsQuantityNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.PreviewRowsQuantityNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.PreviewRowsQuantityNumericUpDown.Name = "PreviewRowsQuantityNumericUpDown";
      this.PreviewRowsQuantityNumericUpDown.Size = new System.Drawing.Size(52, 23);
      this.PreviewRowsQuantityNumericUpDown.TabIndex = 3;
      this.HelpToolTip.SetToolTip(this.PreviewRowsQuantityNumericUpDown, resources.GetString("PreviewRowsQuantityNumericUpDown.ToolTip"));
      this.PreviewRowsQuantityNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.PreviewRowsQuantityNumericUpDown.ValueChanged += new System.EventHandler(this.PreviewRowsQuantityNumericUpDown_ValueChanged);
      // 
      // ColumnOptionsLostWarningLabel
      // 
      this.ColumnOptionsLostWarningLabel.AutoSize = true;
      this.ColumnOptionsLostWarningLabel.BackColor = System.Drawing.Color.Transparent;
      this.ColumnOptionsLostWarningLabel.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ColumnOptionsLostWarningLabel.ForeColor = System.Drawing.Color.Red;
      this.ColumnOptionsLostWarningLabel.Location = new System.Drawing.Point(50, 394);
      this.ColumnOptionsLostWarningLabel.Name = "ColumnOptionsLostWarningLabel";
      this.ColumnOptionsLostWarningLabel.Size = new System.Drawing.Size(282, 12);
      this.ColumnOptionsLostWarningLabel.TabIndex = 15;
      this.ColumnOptionsLostWarningLabel.Text = "Table columns will be recreated so column options changes will be lost.";
      this.ColumnOptionsLostWarningLabel.Visible = false;
      // 
      // ColumnOptionsLostWarningPictureBox
      // 
      this.ColumnOptionsLostWarningPictureBox.BackColor = System.Drawing.Color.Transparent;
      this.ColumnOptionsLostWarningPictureBox.Image = global::MySQL.ForExcel.Properties.Resources.Warning;
      this.ColumnOptionsLostWarningPictureBox.Location = new System.Drawing.Point(27, 389);
      this.ColumnOptionsLostWarningPictureBox.Name = "ColumnOptionsLostWarningPictureBox";
      this.ColumnOptionsLostWarningPictureBox.Size = new System.Drawing.Size(20, 20);
      this.ColumnOptionsLostWarningPictureBox.TabIndex = 47;
      this.ColumnOptionsLostWarningPictureBox.TabStop = false;
      this.ColumnOptionsLostWarningPictureBox.Visible = false;
      // 
      // CreateTableIndexesLastCheckBox
      // 
      this.CreateTableIndexesLastCheckBox.AutoSize = true;
      this.CreateTableIndexesLastCheckBox.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.CreateTableIndexesLastCheckBox.Location = new System.Drawing.Point(73, 365);
      this.CreateTableIndexesLastCheckBox.Name = "CreateTableIndexesLastCheckBox";
      this.CreateTableIndexesLastCheckBox.Size = new System.Drawing.Size(435, 19);
      this.CreateTableIndexesLastCheckBox.TabIndex = 14;
      this.CreateTableIndexesLastCheckBox.Text = "Create table\'s indexes after data has been exported to speed-up rows insertion";
      this.HelpToolTip.SetToolTip(this.CreateTableIndexesLastCheckBox, resources.GetString("CreateTableIndexesLastCheckBox.ToolTip"));
      this.CreateTableIndexesLastCheckBox.UseVisualStyleBackColor = true;
      // 
      // SqlQueriesLabel
      // 
      this.SqlQueriesLabel.AutoSize = true;
      this.SqlQueriesLabel.BackColor = System.Drawing.Color.Transparent;
      this.SqlQueriesLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SqlQueriesLabel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.SqlQueriesLabel.Location = new System.Drawing.Point(24, 311);
      this.SqlQueriesLabel.Name = "SqlQueriesLabel";
      this.SqlQueriesLabel.Size = new System.Drawing.Size(130, 17);
      this.SqlQueriesLabel.TabIndex = 12;
      this.SqlQueriesLabel.Text = "SQL Queries Options";
      // 
      // HelpToolTip
      // 
      this.HelpToolTip.AutoPopDelay = 5000;
      this.HelpToolTip.InitialDelay = 1000;
      this.HelpToolTip.ReshowDelay = 100;
      // 
      // ShowAllDataTypesCheckBox
      // 
      this.ShowAllDataTypesCheckBox.AutoSize = true;
      this.ShowAllDataTypesCheckBox.BackColor = System.Drawing.Color.Transparent;
      this.ShowAllDataTypesCheckBox.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.ShowAllDataTypesCheckBox.Location = new System.Drawing.Point(53, 210);
      this.ShowAllDataTypesCheckBox.Name = "ShowAllDataTypesCheckBox";
      this.ShowAllDataTypesCheckBox.Size = new System.Drawing.Size(387, 19);
      this.ShowAllDataTypesCheckBox.TabIndex = 9;
      this.ShowAllDataTypesCheckBox.Text = "Show all available MySQL data types in the Data Type drop-down list";
      this.HelpToolTip.SetToolTip(this.ShowAllDataTypesCheckBox, resources.GetString("ShowAllDataTypesCheckBox.ToolTip"));
      this.ShowAllDataTypesCheckBox.UseVisualStyleBackColor = false;
      this.ShowAllDataTypesCheckBox.CheckedChanged += new System.EventHandler(this.ShowAllDataTypesCheckBox_CheckedChanged);
      // 
      // GenerateMultipleInsertsCheckBox
      // 
      this.GenerateMultipleInsertsCheckBox.AutoSize = true;
      this.GenerateMultipleInsertsCheckBox.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.GenerateMultipleInsertsCheckBox.Location = new System.Drawing.Point(53, 340);
      this.GenerateMultipleInsertsCheckBox.Name = "GenerateMultipleInsertsCheckBox";
      this.GenerateMultipleInsertsCheckBox.Size = new System.Drawing.Size(281, 19);
      this.GenerateMultipleInsertsCheckBox.TabIndex = 13;
      this.GenerateMultipleInsertsCheckBox.Text = "Generate an INSERT statement for each data row";
      this.HelpToolTip.SetToolTip(this.GenerateMultipleInsertsCheckBox, resources.GetString("GenerateMultipleInsertsCheckBox.ToolTip"));
      this.GenerateMultipleInsertsCheckBox.UseVisualStyleBackColor = true;
      this.GenerateMultipleInsertsCheckBox.CheckedChanged += new System.EventHandler(this.GenerateMultipleInsertsCheckBox_CheckedChanged);
      // 
      // ResetToDefaultsButton
      // 
      this.ResetToDefaultsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.ResetToDefaultsButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ResetToDefaultsButton.Location = new System.Drawing.Point(12, 11);
      this.ResetToDefaultsButton.Name = "ResetToDefaultsButton";
      this.ResetToDefaultsButton.Size = new System.Drawing.Size(128, 23);
      this.ResetToDefaultsButton.TabIndex = 0;
      this.ResetToDefaultsButton.Text = "Reset to Defaults";
      this.ResetToDefaultsButton.UseVisualStyleBackColor = true;
      this.ResetToDefaultsButton.Click += new System.EventHandler(this.ResetToDefaultsButton_Click);
      // 
      // ExportAdvancedOptionsDialog
      // 
      this.AcceptButton = this.DialogAcceptButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.DialogCancelButton;
      this.ClientSize = new System.Drawing.Size(584, 476);
      this.CommandAreaVisible = true;
      this.FootnoteAreaHeight = 0;
      this.MainInstructionLocation = new System.Drawing.Point(13, 21);
      this.Name = "ExportAdvancedOptionsDialog";
      this.Text = "Advanced Options";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExportAdvancedOptionsDialog_FormClosing);
      this.ContentAreaPanel.ResumeLayout(false);
      this.ContentAreaPanel.PerformLayout();
      this.CommandAreaPanel.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.PreviewRowsQuantityNumericUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ColumnOptionsLostWarningPictureBox)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button DialogAcceptButton;
    private System.Windows.Forms.Button DialogCancelButton;
    private System.Windows.Forms.CheckBox UseFormattedValuesCheckBox;
    private System.Windows.Forms.Label FieldDataOptionsLabel;
    private System.Windows.Forms.CheckBox AutoAllowEmptyNonIndexColumnsCheckBox;
    private System.Windows.Forms.CheckBox AutoIndexIntColumnsCheckBox;
    private System.Windows.Forms.CheckBox AddBufferToVarCharCheckBox;
    private System.Windows.Forms.CheckBox DetectDatatypeCheckBox;
    private System.Windows.Forms.Label ColumnDatatypeOptionsLabel;
    private System.Windows.Forms.Label AdvancedExportOptionsLabel;
    private System.Windows.Forms.NumericUpDown PreviewRowsQuantityNumericUpDown;
    private System.Windows.Forms.Label PreviewRowsQuantity2Label;
    private System.Windows.Forms.Label PreviewRowsQuantity1Label;
    private System.Windows.Forms.Label ColumnOptionsLostWarningLabel;
    private System.Windows.Forms.PictureBox ColumnOptionsLostWarningPictureBox;
    private System.Windows.Forms.CheckBox CreateTableIndexesLastCheckBox;
    private System.Windows.Forms.Label SqlQueriesLabel;
    private System.Windows.Forms.ToolTip HelpToolTip;
    private System.Windows.Forms.Button ResetToDefaultsButton;
    private System.Windows.Forms.CheckBox ShowAllDataTypesCheckBox;
    private System.Windows.Forms.CheckBox GenerateMultipleInsertsCheckBox;
  }
}