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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.Types;
using MySQL.ForExcel.Classes.EventArguments;
using MySQL.ForExcel.Properties;
using MySQL.Utility.Classes;
using Excel = Microsoft.Office.Interop.Excel;

namespace MySQL.ForExcel.Classes
{
  /// <summary>
  /// Represents the schema of a MySQL table's column.
  /// </summary>
  public class MySqlDataColumn : DataColumn, INotifyPropertyChanged
  {
    #region Constants

    /// <summary>
    /// Column attribute to declare it as auto increment.
    /// </summary>
    public const string ATTRIBUTE_AUTO_INCREMENT = "AUTO_INCREMENT";

    /// <summary>
    /// Column attribute to declare it as the automatically generated Primary Key column.
    /// </summary>
    public const string ATTRIBUTE_AUTO_PK = "AUTO_PK";

    /// <summary>
    /// Column attribute to declare its default value.
    /// </summary>
    public const string ATTRIBUTE_DEFAULT = "DEFAULT";

    /// <summary>
    /// Column attribute to exclude the column from operations.
    /// </summary>
    public const string ATTRIBUTE_EXCLUDE = "EXCLUDE";

    /// <summary>
    /// Key used to represent a warning about the column's data not being suitable for the data type in the Append Data dialog.
    /// </summary>
    private const string DATA_NOT_SUITABLE_APPEND_WARNING_KEY = "DATA_NOT_SUITABLE_APPEND";

    /// <summary>
    /// Key used to represent a warning about the column's data not being suitable for the data type in the Export Data dialog.
    /// </summary>
    private const string DATA_NOT_SUITABLE_EXPORT_WARNING_KEY = "DATA_NOT_SUITABLE_EXPORT";

    /// <summary>
    /// Key used to represent a warning about the column's data not being unique.
    /// </summary>
    private const string DATA_NOT_UNIQUE_WARNING_KEY = "DATA_NOT_UNIQUE";

    /// <summary>
    /// Key used to represent a warning about the column's name being a duplicate of one in another column.
    /// </summary>
    private const string DUPLICATE_NAME_WARNING_KEY = "DUPLICATE_NAME";

    /// <summary>
    /// Key used to represent a warning about the column's name being null or empty.
    /// </summary>
    private const string EMPTY_NAME_WARNING_KEY = "EMPTY_NAME";

    /// <summary>
    /// Key used to represent a warning about the column's data type being a MySQL invalid one.
    /// </summary>
    private const string INVALID_DATA_TYPE_WARNING_KEY = "INVALID_DATA_TYPE";

    /// <summary>
    /// Key used to represent a warning about the column's default value being an invalid one for its data type.
    /// </summary>
    private const string INVALID_DEFAULT_VALUE_WARNING_KEY = "INVALID_DEFAULT_VALUE";

    /// <summary>
    /// Key used to represent a warning about the column's data type declaraton for an ENUM or SET being incorrect due to a specific element.
    /// </summary>
    private const string INVALID_SET_ENUM_WARNING_KEY = "INVALID_SET_ENUM";

    /// <summary>
    /// Key used to represent a warning about the column's data type being null or empty.
    /// </summary>
    private const string NO_DATA_TYPE_WARNING_KEY = "NO_DATA_TYPE";

    /// <summary>
    /// Key used to represent a warning about the table indexes being created after the data is exported, which affects Auto Increment column declarations.
    /// </summary>
    private const string TABLE_INDEXES_AFTER_EXPORT_WARNING_KEY = "TABLE_INDEXES_AFTER_EXPORT";

    #endregion Constants

    #region Fields

    /// <summary>
    /// Flag indicating whether the column will accept null values.
    /// </summary>
    private bool _allowNull;

    /// <summary>
    /// Flag indicating whether the column automatically increments the value of the column for new rows added to the table.
    /// </summary>
    private bool _autoIncrement;

    /// <summary>
    /// The <see cref="DataColumn.ColumnName"/> escaping the back-tick character.
    /// </summary>
    private string _columnNameForSqlQueries;

    /// <summary>
    /// Flag indicating whether this column has an index automatically created for it.
    /// </summary>
    private bool _createIndex;

    /// <summary>
    /// Flag indicating whether this column will be excluded from the list of columns to be created on a new table's creation.
    /// </summary>
    private bool _excludeColumn;

    /// <summary>
    /// The ordinal index of the column in a source <see cref="MySqlDataTable"/> from which data will be appended from.
    /// </summary>
    private int _mappedDataColOrdinal;

    /// <summary>
    /// Flag indicating whether the column is part of the primary key.
    /// </summary>
    private bool _primaryKey;

    /// <summary>
    /// The consistent <see cref="MySqlDataType"/> that can hold the data for all rows starting from the first row.
    /// </summary>
    private MySqlDataType _rowsFromFirstDataType;

    /// <summary>
    /// The consistent <see cref="MySqlDataType"/> that can hold the data for all rows starting from the second row.
    /// </summary>
    private MySqlDataType _rowsFromSecondDataType;

    /// <summary>
    /// Flag indicating if the column has a related unique index.
    /// </summary>
    private bool _uniqueKey;

    /// <summary>
    /// The user speficied default value for the column.
    /// </summary>
    private string _userDefaultValue;

    /// <summary>
    /// Container with warnings for users about the column properties that could cause errors when creating this column in a database table.
    /// </summary>
    private WarningsContainer _warnings;

    /// <summary>
    /// Dictionary with additional information related to specific warnings.
    /// </summary>
    private Dictionary<string, Tuple<string, string>> _warningsMoreInfosDictionary;

    #endregion Fields

    /// <summary>
    /// Initializes a new instance of the <see cref="MySqlDataColumn"/> class.
    /// </summary>
    public MySqlDataColumn()
    {
      _autoIncrement = false;
      _allowNull = false;
      _columnNameForSqlQueries = null;
      _excludeColumn = false;
      _mappedDataColOrdinal = -1;
      _userDefaultValue = null;
      AutoIncrement = false;
      AutoPk = false;
      CharSet = null;
      Collation = null;
      DisplayName = string.Empty;
      DuplicateGroupsFound = 0;
      InExportMode = false;
      IsDisplayNameDuplicate = false;
      MappedDataColName = null;
      MySqlDataType = null;
      PrimaryKey = false;
      RangeColumnIndex = 0;
      RowsFromFirstDataType = null;
      RowsFromSecondDataType = null;
      SetupWarnings();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MySqlDataColumn"/> class.
    /// </summary>
    /// <param name="inExportMode">Flag indicating if the column is being constructed for exporting it to a new MySQL table.</param>
    /// <param name="autoPk">Flag indicating whether the column is used in an auto-generated primary key.</param>
    /// <param name="columnName">The name of the column.</param>
    /// <param name="rangeColumnIndex">The index of the Excel range column used to populate this column with data.</param>
    public MySqlDataColumn(bool inExportMode, bool autoPk, string columnName, int rangeColumnIndex)
      : this()
    {
      InExportMode = inExportMode;
      AutoPk = autoPk;
      if (AutoPk)
      {
        AutoIncrement = true;
        PrimaryKey = true;
        SetMySqlDataType(new MySqlDataType("Integer", true));
      }

      ColumnName = autoPk ? "AutoPK" : columnName;
      DisplayName = ColumnName;
      RangeColumnIndex = autoPk ? 0 : rangeColumnIndex;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MySqlDataColumn"/> class.
    /// </summary>
    /// <param name="columnName">Name of the column.</param>
    /// <param name="mySqlFullDataType">Data type for a table column supported by MySQL Server.</param>
    /// <param name="charSet">The character set used to store text data in this column.</param>
    /// <param name="collation">The collation used with the character set to store text data in this column.</param>
    /// <param name="datesAsMySqlDates">Flag indicating if the data type for dates will be stored as <see cref="MySql.Data.Types.MySqlDateTime"/>
    /// or <see cref="System.DateTime"/>.</param>
    /// <param name="allowNulls">Flag indicating if the column will accept null values.</param>
    /// <param name="keyInfo">Information about the type of key this column belongs to.</param>
    /// <param name="extraInfo">Extra information related to the column's data type as stored by the MySQL server.</param>
    public MySqlDataColumn(string columnName, string mySqlFullDataType, string charSet, string collation, bool datesAsMySqlDates, bool allowNulls, string keyInfo, string extraInfo)
      : this()
    {
      DisplayName = ColumnName = columnName;
      AllowNull = allowNulls;
      CharSet = charSet;
      Collation = collation;
      SetMySqlDataType(new MySqlDataType(mySqlFullDataType, true, datesAsMySqlDates));
      DataType = MySqlDataType.DotNetType;
      CreateIndex = keyInfo.Equals("mul", StringComparison.InvariantCultureIgnoreCase);
      PrimaryKey = keyInfo.Equals("pri", StringComparison.InvariantCultureIgnoreCase);
      UniqueKey = keyInfo.Equals("uni", StringComparison.InvariantCultureIgnoreCase);
      if (string.IsNullOrEmpty(extraInfo))
      {
        return;
      }

      AutoIncrement = extraInfo.Contains(ATTRIBUTE_AUTO_INCREMENT, StringComparison.InvariantCultureIgnoreCase);
      AutoPk = extraInfo.Contains(ATTRIBUTE_AUTO_PK, StringComparison.InvariantCultureIgnoreCase);
      ExcludeColumn = extraInfo.Contains(ATTRIBUTE_EXCLUDE, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MySqlDataColumn"/> class.
    /// </summary>
    /// <param name="columnName">Name of the column.</param>
    /// <param name="mySqlFullDataType">Data type for a table column supported by MySQL Server.</param>
    /// <param name="datesAsMySqlDates">Flag indicating if the data type for dates will be stored as <see cref="MySql.Data.Types.MySqlDateTime"/>
    /// or <see cref="System.DateTime"/>.</param>
    public MySqlDataColumn(string columnName, string mySqlFullDataType, bool datesAsMySqlDates)
      : this(columnName, mySqlFullDataType, null, null, datesAsMySqlDates, false, string.Empty, string.Empty)
    {
    }

    #region Enumerations

    /// <summary>
    /// Describes the type of a MySQL collecion data type.
    /// </summary>
    public enum CollectionDataType
    {
      /// <summary>
      /// An Enum type where only 1 value from a valid values list is stored.
      /// </summary>
      Enum,

      /// <summary>
      /// A Set type where a set of N values from a valid values list is stored.
      /// </summary>
      Set
    }

    /// <summary>
    /// Describes the data type to use depending on the ordinal of the current table row.
    /// </summary>
    public enum MySqlDataTypeFromRowType
    {
      /// <summary>
      /// Use the data type detected from the first row to the end.
      /// </summary>
      FromFirst,

      /// <summary>
      /// Use the data type detected from the second row to the end.
      /// </summary>
      FromSecond
    }

    #endregion Enumerations

    #region Properties

    /// <summary>
    /// Gets the collation used to store text data in this column, looking up if not defined at this element.
    /// </summary>
    public string AbsoluteCollation
    {
      get
      {
        return string.IsNullOrEmpty(Collation) ? ParentTable.AbsoluteCollation : Collation;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the column will accept null values.
    /// </summary>
    public bool AllowNull
    {
      get
      {
        return _allowNull;
      }

      set
      {
        bool valueChanged = _allowNull != value;
        _allowNull = value;
        if (!valueChanged || !InExportMode)
        {
          return;
        }

        OnPropertyChanged("AllowNull");
        if (_uniqueKey)
        {
          UpdateDataUniquenessWarnings();
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the column automatically increments the value of the column for new rows added to the table.
    /// </summary>
    public new bool AutoIncrement
    {
      get
      {
        return InExportMode ? _autoIncrement : base.AutoIncrement;
      }

      set
      {
        bool valueChanged = false;
        if (InExportMode)
        {
          valueChanged = _autoIncrement != value;
          _autoIncrement = value;
        }
        else
        {
          try
          {
            valueChanged = base.AutoIncrement != value;
            base.AutoIncrement = value;
          }
          catch (Exception ex)
          {
            MiscUtilities.ShowCustomizedErrorDialog("AutoIncrement set error.", ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace, true);
            MySqlSourceTrace.WriteAppErrorToLog(ex);
          }
        }

        if (!valueChanged || !InExportMode)
        {
          return;
        }

        UpdateAutoIncrementWarning();
        OnPropertyChanged("AutoIncrement");
      }
    }

    /// <summary>
    /// Gets a value indicating whether the column is used in an auto-generated primary key.
    /// </summary>
    public bool AutoPk { get; private set; }

    /// <summary>
    /// Gets or sets the character set used to store text data in this column.
    /// </summary>
    /// <remarks>If null or empty it means the parent table character set is used.</remarks>
    public string CharSet { get; set; }

    /// <summary>
    /// Gets or sets the collation used with the character set to store text data in this column.
    /// </summary>
    /// <remarks>If null or empty it means the default collation is used.</remarks>
    public string Collation { get; set; }

    /// <summary>
    /// Gets or sets the name of the column in the <see cref="DataColumnCollection"/>.
    /// </summary>
    public new string ColumnName
    {
      get
      {
        return base.ColumnName;
      }

      set
      {
        if (AutoPk)
        {
          value = "AutoPK";
        }

        base.ColumnName = value;
        _columnNameForSqlQueries = null;
      }
    }

    /// <summary>
    /// Gets the <see cref="DataColumn.ColumnName"/> escaping the back-tick character.
    /// </summary>
    public string ColumnNameForSqlQueries
    {
      get
      {
        return _columnNameForSqlQueries ?? (_columnNameForSqlQueries = ColumnName.Replace("`", "``"));
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this column has an index automatically created for it.
    /// </summary>
    public bool CreateIndex
    {
      get
      {
        return _createIndex;
      }

      set
      {
        bool valueChanged = _createIndex != value;
        _createIndex = value;
        if (!valueChanged || !InExportMode)
        {
          return;
        }

        if (!PrimaryKey && ParentTable != null && (!_createIndex && ParentTable.AutoAllowEmptyNonIndexColumns))
        {
          AllowNull = true;
        }

        OnPropertyChanged("CreateIndex");
      }
    }

    /// <summary>
    /// Gets the last warning text associated to this column.
    /// </summary>
    public string CurrentWarningText
    {
      get
      {
        string currentWarningText = _warnings.CurrentWarningText;
        return ExcludeColumn || string.IsNullOrEmpty(currentWarningText)
          ? string.Empty
          : currentWarningText;
      }
    }

    /// <summary>
    /// Gets a tuple containing a title and description texts, of additional information related to the <see cref="CurrentWarningText"/>.
    /// </summary>
    public Tuple<string, string> CurrentWarningMoreInfo
    {
      get
      {
        return ExcludeColumn || string.IsNullOrEmpty(_warnings.CurrentWarningKey) || !_warningsMoreInfosDictionary.ContainsKey(_warnings.CurrentWarningKey)
          ? null
          : _warningsMoreInfosDictionary[_warnings.CurrentWarningKey];
      }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> used for dates will be stored as <see cref="MySql.Data.Types.MySqlDateTime"/> or as <see cref="DateTime"/>.
    /// </summary>
    public bool DatesAsMySqlDates
    {
      get
      {
        return MySqlDataType == null || MySqlDataType.DatesAsMySqlDates;
      }
    }

    /// <summary>
    /// Gets the name for this column, when its value is different than the one in <see cref="DataColumn.ColumnName"/>
    /// it means the latter represents an internal name and this property holds the real column name.
    /// </summary>
    public string DisplayName { get; private set; }

    /// <summary>
    /// Gets the <see cref="DisplayName"/> escaping the back-tick character.
    /// </summary>
    public string DisplayNameForSqlQueries
    {
      get
      {
        return DisplayName.Replace("`", "``");
      }
    }

    /// <summary>
    /// Gets the number of duplicate groups found when doing a unique data check.
    /// </summary>
    public int DuplicateGroupsFound { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether this column will be excluded from the list of columns to be created on a new table's creation.
    /// </summary>
    public bool ExcludeColumn
    {
      get
      {
        return _excludeColumn;
      }

      set
      {
        bool valueChanged = _excludeColumn != value;
        _excludeColumn = value;
        if (!valueChanged || !InExportMode)
        {
          return;
        }

        if (_excludeColumn && !AutoPk && PrimaryKey)
        {
          PrimaryKey = false;
        }

        OnColumnWarningsChanged();
        OnPropertyChanged("ExcludeColumn");

        // Fire a duplicates check on all columns to update those warnings, since now that this column is
        // being excluded it should not be considered a duplicate of others.
        if (Table is MySqlDataTable)
        {
          ParentTable.CheckForDuplicatedColumnDisplayNames();
        }
      }
    }

    /// <summary>
    /// Gets a value indicating whether the column is included for INSERT queries.
    /// </summary>
    public bool IncludeForInsertion
    {
      get
      {
        return !ExcludeColumn && (!ParentTable.OperationType.IsForAppend() || MappedDataColOrdinal >= 0);
      }
    }

    /// <summary>
    /// Gets a value indicating whether the column is being constructed for exporting it to a new MySQL table.
    /// </summary>
    public bool InExportMode { get; private set; }

    /// <summary>
    /// Gets a value indicating if the <see cref="DisplayName"/> property value is not a duplicate of the one in another column.
    /// </summary>
    public bool IsDisplayNameDuplicate { get; private set; }

    /// <summary>
    /// Gets or sets the name of the column in a source <see cref="MySqlDataTable"/> from which data will be appended from.
    /// </summary>
    public string MappedDataColName { get; set; }

    /// <summary>
    /// Gets or sets the ordinal index of the column in a source <see cref="MySqlDataTable"/> from which data will be appended from.
    /// </summary>
    public int MappedDataColOrdinal
    {
      get
      {
        return _mappedDataColOrdinal;
      }

      set
      {
        _mappedDataColOrdinal = value;
        OnPropertyChanged("MappedDataColOrdinal");
      }
    }

    /// <summary>
    /// Gets the corresponding data type supported by MySQL Server for this column.
    /// </summary>
    public MySqlDataType MySqlDataType { get; private set; }

    /// <summary>
    /// Gets the parent table of this column as a <see cref="MySqlDataTable"/> object.
    /// </summary>
    public MySqlDataTable ParentTable
    {
      get
      {
        return Table as MySqlDataTable;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the column is part of the primary key.
    /// </summary>
    public bool PrimaryKey
    {
      get
      {
        return _primaryKey;
      }

      set
      {
        bool valueChanged = _primaryKey != value;
        _primaryKey = value;
        if (!valueChanged || !InExportMode)
        {
          return;
        }

        if (_primaryKey)
        {
          CreateIndex = false;
          UniqueKey = false;
          AllowNull = false;
        }

        OnPropertyChanged("PrimaryKey");
      }
    }

    /// <summary>
    /// Gets the index of the Excel range column used to populate this column with data.
    /// </summary>
    public int RangeColumnIndex { get; private set; }

    /// <summary>
    /// Gets the consistent <see cref="MySqlDataType"/> that can hold the data for all rows starting from the first row.
    /// </summary>
    public MySqlDataType RowsFromFirstDataType
    {
      get
      {
        return _rowsFromFirstDataType;
      }

      set
      {
        _rowsFromFirstDataType = value ?? DefaultDataTypeForEmptyColumns;
      }
    }

    /// <summary>
    /// Gets the consistent <see cref="MySqlDataType"/> that can hold the data for all rows starting from the second row.
    /// </summary>
    public MySqlDataType RowsFromSecondDataType
    {
      get
      {
        return _rowsFromSecondDataType;
      }

      set
      {
        _rowsFromSecondDataType = value ?? DefaultDataTypeForEmptyColumns;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the column has a related unique index.
    /// </summary>
    public bool UniqueKey
    {
      get
      {
        return _uniqueKey;
      }

      set
      {
        bool valueChanged = _uniqueKey != value;
        _uniqueKey = value;
        if (!valueChanged || !InExportMode)
        {
          return;
        }

        if (_uniqueKey)
        {
          CreateIndex = true;
          PrimaryKey = false;
        }

        UpdateDataUniquenessWarnings();
        OnPropertyChanged("UniqueKey");
      }
    }

    /// <summary>
    /// Gets or sets the user speficied default value for the column.
    /// </summary>
    public string UserDefaultValue
    {
      get
      {
        return _userDefaultValue;
      }

      set
      {
        bool valueChanged = _userDefaultValue != value;
        _userDefaultValue = value;
        if (!valueChanged || !InExportMode)
        {
          return;
        }

        OnPropertyChanged("UserDefaultValue");
      }
    }

    /// <summary>
    /// Gets a <see cref="MySqlDataType"/> used as a default for columns with no data.
    /// </summary>
    private MySqlDataType DefaultDataTypeForEmptyColumns
    {
      get
      {
        return new MySqlDataType(null, true, DatesAsMySqlDates);
      }
    }

    #endregion Properties

    #region Events

    /// <summary>
    /// Delegate handler for the <see cref="ColumnWarningsChanged"/> event.
    /// </summary>
    /// <param name="sender">Sender object.</param>
    /// <param name="args">Event arguments.</param>
    public delegate void ColumnWarningsChangedEventHandler(object sender, ColumnWarningsChangedArgs args);

    /// <summary>
    /// Occurs when the warnings associated to this column change.
    /// </summary>
    public event ColumnWarningsChangedEventHandler ColumnWarningsChanged;

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion Events

    /// <summary>
    /// Checks if the current data in this column could be stored in the target column.
    /// </summary>
    /// <param name="targetColumn">A target <see cref="MySqlDataColumn"/> where the data on this column would be stored.</param>
    /// <returns><c>true</c> if the current data in this column could be stored in the target column, <c>false</c> otherwise.</returns>
    public bool CanDataBeStoredInGivenColumn(MySqlDataColumn targetColumn)
    {
      if (targetColumn == null)
      {
        return false;
      }

      // Test the data on each row of the parent table in this specific column to check if it could be stored in the target column
      bool dataFitsIntoType = true;
      MySqlDataTable parentTable = Table as MySqlDataTable;
      int rowIdx = 0;
      if (parentTable != null)
      {
        foreach (string strValueFromArray in parentTable.Rows.Cast<DataRow>()
            .Where(dr => !parentTable.FirstRowContainsColumnNames || rowIdx++ != 0)
            .Select(dr => dr[Ordinal].ToString())
            .Where(strValueFromArray => strValueFromArray.Length != 0))
        {
          dataFitsIntoType = targetColumn.MySqlDataType.CanStoreValue(strValueFromArray);

          // If found a value where the data type is not good for it break since there is no need testing more values.
          if (!dataFitsIntoType)
          {
            break;
          }
        }
      }

      // Update warning stating the column's data type is not suitable for all of its data (in the preview table)
      // either for the Append or Export Data operation.
      string warningKey = targetColumn.ParentTable != null && targetColumn.ParentTable.OperationType.IsForAppend()
        ? DATA_NOT_SUITABLE_APPEND_WARNING_KEY
        : DATA_NOT_SUITABLE_EXPORT_WARNING_KEY;
      if (targetColumn._warnings.SetVisibility(warningKey, !dataFitsIntoType))
      {
        targetColumn.OnColumnWarningsChanged();
      }

      return dataFitsIntoType;
    }

    /// <summary>
    /// Checks whether another column in the <see cref="ParentTable"/> has the same <see cref="DisplayName"/> as this column.
    /// </summary>
    public void CheckForDuplicatedDisplayName()
    {
      if (!(Table is MySqlDataTable))
      {
        return;
      }

      IsDisplayNameDuplicate = ParentTable.GetColumnIndex(DisplayName, true, false, true, Ordinal) > -1;
      if (AutoPk)
      {
        // Update warning on the parent table regarding the AutoPK column name being a duplicate of another existing column's name
        ParentTable.UpdateAutoPkWarning(IsDisplayNameDuplicate);
      }
      else
      {
        // Update warning stating the column name is a duplicate of another existing column's name
        if (_warnings.SetVisibility(DUPLICATE_NAME_WARNING_KEY, IsDisplayNameDuplicate))
        {
          OnColumnWarningsChanged();
        }
      }
    }

    /// <summary>
    /// Runs a check on the column's data to determine if it is unique (i.e. there are no data duplicates).
    /// </summary>
    /// <remarks>If the <see cref="AllowNull"/> value is <c>true</c> then nulls are considered as allowed duplicate values in MySQL, so uniqueness is checked only for non null values.</remarks>
    /// <returns><c>true</c> if the data in the column is unique, <c>false</c> otherwise.</returns>
    public bool CheckForDataUniqueness()
    {
      var duplicates = GetDuplicateValuesInColumn();
      if (duplicates == null)
      {
        DuplicateGroupsFound = 0;
        _warningsMoreInfosDictionary.Remove(DATA_NOT_UNIQUE_WARNING_KEY);
      }
      else
      {
        DuplicateGroupsFound = duplicates.Count;
        var moreInfoTextBuilder = new StringBuilder(DuplicateGroupsFound * byte.MaxValue);
        foreach (var dictPair in duplicates)
        {
          moreInfoTextBuilder.AppendLine(string.Format("{0} ({1})", dictPair.Key, dictPair.Value));
        }

        var moreInfoTuple = new Tuple<string, string>(Resources.ColumnDataNotUniqueMoreInfoTitle, moreInfoTextBuilder.ToString());
        if (_warningsMoreInfosDictionary.ContainsKey(DATA_NOT_UNIQUE_WARNING_KEY))
        {
          _warningsMoreInfosDictionary[DATA_NOT_UNIQUE_WARNING_KEY] = moreInfoTuple;
        }
        else
        {
          _warningsMoreInfosDictionary.Add(DATA_NOT_UNIQUE_WARNING_KEY, moreInfoTuple);
        }
      }

      return DuplicateGroupsFound == 0;
    }

    /// <summary>
    /// Clears all warnings from this column.
    /// </summary>
    public void ClearWarnings()
    {
      _warnings.Clear();
    }

    /// <summary>
    /// Creates a new <see cref="MySqlDataColumn"/> object with a schema identical to this column's schema.
    /// </summary>
    /// <returns>A new <see cref="MySqlDataColumn"/> object with a schema cloned from this column.</returns>
    public MySqlDataColumn CloneSchema()
    {
      MySqlDataColumn clonedColumn = new MySqlDataColumn { ColumnName = ColumnName };
      clonedColumn.SyncSchema(this);
      return clonedColumn;
    }

    /// <summary>
    /// Analyzes the data stored in this column and automatically detects the MySQL data type for it.
    /// </summary>
    /// <param name="dataRange">Excel data range containing the data to fill the table.</param>
    /// <param name="cropRange">Attempts to crop the data range to a subrange containing only formulas or constants.</param>
    /// <remarks>
    /// The data type detection cannot be done with the data already stored in the column's table, the columns which need data type detection
    /// are created by default with a string data type, when Excel data is inserted to it it is already converted to string so the data type
    /// detection must be done using the original Excel data.
    /// </remarks>
    public void DetectMySqlDataType(Excel.Range dataRange, bool cropRange)
    {
      if (AutoPk || ParentTable == null || ParentTable.Rows.Count == 0 || dataRange == null)
      {
        return;
      }

      Excel.Range columnRange = dataRange.Columns[RangeColumnIndex];
      if (cropRange)
      {
        columnRange = columnRange.GetNonEmptyRange();
      }

      if (columnRange == null)
      {
        return;
      }

      var useFormattedValues = ParentTable.IsFormatted;
      var typesListForFirstAndRest = new List<MySqlDataType>(2);
      var typesListFromSecondRow = new List<MySqlDataType>(Table.Rows.Count);
      long maxTextLengthForFirstRow = 0;
      long maxTextLengthFromSecondRow = 0;
      bool addBufferToVarChar = ParentTable.AddBufferToVarChar;
      bool datesAsMySqlDates = DatesAsMySqlDates;
      for (int rowPos = 1; rowPos <= columnRange.Rows.Count; rowPos++)
      {
        Excel.Range excelCell = columnRange.Cells[rowPos, 1];
        object rawValue = excelCell != null ? excelCell.GetCellPackedValue(useFormattedValues) : null;
        if (rawValue == null)
        {
          continue;
        }

        // Treat always as a Varchar value first in case all rows do not have a consistent datatype just to see the varchar len calculated by GetMySQLExportDataType
        string valueAsString = rawValue.ToString();
        bool valueOverflow;
        var proposedType = MySqlDataType.DetectDataType(valueAsString, out valueOverflow, datesAsMySqlDates);
        if (proposedType.IsBool)
        {
          proposedType = new MySqlDataType("VarChar(5)", true, datesAsMySqlDates);
        }
        else if (proposedType.IsDateBased)
        {
          proposedType = new MySqlDataType(string.Format("VarChar({0})", valueAsString.Length), true, datesAsMySqlDates);
        }

        if (!proposedType.IsText)
        {
          var typeLength = addBufferToVarChar && proposedType.Length > 0 ? proposedType.Length : valueAsString.Length;
          maxTextLengthFromSecondRow = Math.Max(typeLength, maxTextLengthFromSecondRow);
        }

        // Normal datatype detection
        proposedType = MySqlDataType.DetectDataType(rawValue, out valueOverflow, datesAsMySqlDates);

        if (rowPos == 1)
        {
          typesListForFirstAndRest.Add(proposedType);
          maxTextLengthForFirstRow = maxTextLengthFromSecondRow;
          maxTextLengthFromSecondRow = 0;
        }
        else
        {
          typesListFromSecondRow.Add(proposedType);
        }
      }

      if (typesListFromSecondRow.Count + typesListForFirstAndRest.Count == 0)
      {
        // There is no data on the column, so set the data types to the default for empty columns.
        RowsFromFirstDataType = DefaultDataTypeForEmptyColumns;
        RowsFromSecondDataType = DefaultDataTypeForEmptyColumns;
      }
      else
      {
        // Get the consistent DataType for all rows except first one.
        RowsFromSecondDataType = GetConsistentDataTypeOnAllRows(typesListFromSecondRow, maxTextLengthFromSecondRow);
        if (typesListFromSecondRow.Count > 0)
        {
          typesListForFirstAndRest.Add(RowsFromSecondDataType);
        }

        // Get the consistent DataType between first row and the previously computed consistent DataType for the rest of the rows.
        RowsFromFirstDataType = GetConsistentDataTypeOnAllRows(typesListForFirstAndRest, Math.Max(maxTextLengthForFirstRow, maxTextLengthFromSecondRow));
      }

      // Set the MySqlDataType using the string type in order to run logic that automatically sets column flags depending on the detected type.
      SetMySqlDataType(ParentTable.FirstRowContainsColumnNames ? RowsFromSecondDataType.FullType : RowsFromFirstDataType.FullType, true, false);
    }

    /// <summary>
    /// Fills a given <see cref="DataRow"/> with schema information about this column.
    /// </summary>
    /// <param name="schemaInfoRow">A <see cref="DataRow"/> formatted to hold Field, Type, Null, Key, Default and Extra information.</param>
    public void FillSchemaInfoRow(ref DataRow schemaInfoRow)
    {
      if (schemaInfoRow == null)
      {
        return;
      }

      var extraBuilder = new StringBuilder();
      schemaInfoRow["Name"] = DisplayName;
      schemaInfoRow["Type"] = MySqlDataType;
      schemaInfoRow["Null"] = AllowNull ? "YES" : "NO";
      if (PrimaryKey)
      {
        schemaInfoRow["Key"] = "PRI";
      }
      else if (UniqueKey)
      {
        schemaInfoRow["Key"] = "UNI";
      }
      else if (CreateIndex)
      {
        schemaInfoRow["Key"] = "MUL";
      }

      schemaInfoRow["Default"] = UserDefaultValue;
      schemaInfoRow["CharSet"] = CharSet;
      schemaInfoRow["Collation"] = Collation;
      if (AutoIncrement)
      {
        extraBuilder.Append(ATTRIBUTE_AUTO_INCREMENT);
      }

      if (AutoPk)
      {
        if (extraBuilder.Length > 0)
        {
          extraBuilder.Append(" ");
        }

        extraBuilder.Append(ATTRIBUTE_AUTO_PK);
      }

      if (ExcludeColumn)
      {
        if (extraBuilder.Length > 0)
        {
          extraBuilder.Append(" ");
        }

        extraBuilder.Append(ATTRIBUTE_EXCLUDE);
      }

      schemaInfoRow["Extra"] = extraBuilder.ToString();
      extraBuilder.Clear();
    }

    /// <summary>
    /// Gets a dictionary of duplicate values within the column's rows and the count of each duplicate value.
    /// </summary>
    /// <remarks>If the <see cref="AllowNull"/> value is <c>true</c> then nulls are considered as allowed duplicate values in MySQL, so uniqueness is checked only for non null values.</remarks>
    /// <returns>A dictionary of duplicate values within the column's rows and the count of each duplicate value.</returns>
    public Dictionary<object, int> GetDuplicateValuesInColumn()
    {
      if (Table == null || Table.Rows.Count == 0)
      {
        return null;
      }

      var duplicates = new Dictionary<object, int>(Table.Rows.Count);
      foreach (
        var group in
          Table.Rows.Cast<DataRow>()
            .Select(row => row[Ordinal])
            .GroupBy(value => value)
            .Where(group => group.Count() > 1)
            .Where(group => !_allowNull || group.Key != DBNull.Value))
      {
        duplicates.Add(group.Key, group.Count());
      }

      return duplicates;
    }

    /// <summary>
    /// Gets a string representation of a raw value formatted so the value can be inserted in this column.
    /// </summary>
    /// <param name="rawValue">The raw value to be inserted.</param>
    /// <param name="escapeStringForTextTypes">Flag indicating whether text values must have special characters escaped with a back-slash.</param>
    /// <returns>The formatted string representation of the raw value.</returns>
    public object GetInsertingValueForType(object rawValue, bool escapeStringForTextTypes)
    {
      if (MySqlDataType == null)
      {
        return rawValue;
      }

      // Return values for empty raw values
      bool nullRawValue = rawValue == null || rawValue == DBNull.Value;
      if (nullRawValue)
      {
        if (AllowNull)
        {
          return DBNull.Value;
        }

        if (MySqlDataType.IsNumeric || MySqlDataType.IsBinary)
        {
          return 0;
        }

        if (!MySqlDataType.IsDateBased)
        {
          return MySqlDataType.RequiresQuotesForValue ? string.Empty : rawValue;
        }
      }

      // Return values for raw values with data
      if (MySqlDataType.IsDateBased)
      {
        return MySqlDataType.GetValueAsDateTime(rawValue);
      }

      if (MySqlDataType.IsBool)
      {
        return MySqlDataType.GetValueAsBoolean(rawValue);
      }

      if (MySqlDataType.RequiresQuotesForValue)
      {
        return rawValue == null ? null : (escapeStringForTextTypes ? rawValue.ToString().EscapeDataValueString() : rawValue.ToString());
      }

      return rawValue;
    }

    /// <summary>
    /// Gets a string representation for null date values.
    /// </summary>
    /// <param name="allowsNull">Flag indicating if the column allows null values.</param>
    /// <returns>A string representation for null date values.</returns>
    public string GetNullDateValueAsString(out bool allowsNull)
    {
      allowsNull = AllowNull;
      return allowsNull ? "null" : MySqlDataType.MYSQL_EMPTY_DATE;
    }

    /// <summary>
    /// Creates a SQL query fragment meant to be used within a CREATE TABLE statement to create a column with this column's schema.
    /// </summary>
    /// <returns>A SQL query fragment describing this column's schema.</returns>
    public string GetSql()
    {
      if (string.IsNullOrEmpty(DisplayName))
      {
        return null;
      }

      var colDefinitionBuilder = new StringBuilder();
      colDefinitionBuilder.AppendFormat("`{0}` {1}", DisplayName.Replace("`", "``"), MySqlDataType.FullTypeSql);
      colDefinitionBuilder.AppendFormat(" {0}NULL", AllowNull ? string.Empty : "NOT ");
      if (!string.IsNullOrEmpty(UserDefaultValue))
      {
        bool isDefaultValueCurrentTimestamp = UserDefaultValue.Equals(MySqlDataType.ATTRIBUTE_CURRENT_TIMESTAMP, StringComparison.InvariantCultureIgnoreCase);
        colDefinitionBuilder.AppendFormat(" {0} {1}{2}{1}",
          ATTRIBUTE_DEFAULT,
          MySqlDataType.RequiresQuotesForValue && !isDefaultValueCurrentTimestamp ? "'" : string.Empty,
          UserDefaultValue);
      }

      if (AutoIncrement)
      {
        colDefinitionBuilder.Append(" ");
        colDefinitionBuilder.Append(ATTRIBUTE_AUTO_INCREMENT);
      }

      return colDefinitionBuilder.ToString();
    }

    /// <summary>
    /// Gets a text value from a raw value (object) converted to the data value of a specific target column.
    /// </summary>
    /// <param name="rawValue">The raw value.</param>
    /// <param name="valueIsNull">Output flag indicating whether the raw value is a null one.</param>
    /// <returns>The text representation of the raw value.</returns>
    public string GetStringValue(object rawValue, out bool valueIsNull)
    {
      string valueToDb = "null";
      object valueObject = GetInsertingValueForType(rawValue, true);
      valueIsNull = valueObject == null || valueObject == DBNull.Value;
      if (valueIsNull)
      {
        return MySqlDataType.IsDateBased
          ? GetNullDateValueAsString(out valueIsNull)
          : valueToDb;
      }

      if (valueObject is DateTime)
      {
        var dtValue = (DateTime)valueObject;
        valueToDb = dtValue.Equals(DateTime.MinValue)
          ? GetNullDateValueAsString(out valueIsNull)
          : dtValue.ToString(MySqlDataType.MYSQL_DATE_FORMAT);
      }
      else if (valueObject is MySqlDateTime)
      {
        var dtValue = (MySqlDateTime)valueObject;
        valueToDb = !dtValue.IsValidDateTime || dtValue.GetDateTime().Equals(DateTime.MinValue)
          ? GetNullDateValueAsString(out valueIsNull)
          : dtValue.GetDateTime().ToString(MySqlDataType.MYSQL_DATE_FORMAT);
      }
      else
      {
        valueToDb = MySqlDataType.GetStringRepresentationForNumericObject(valueObject);
      }

      return valueToDb;
    }

    /// <summary>
    /// Sets the <see cref="UserDefaultValue"/> property to the given display name.
    /// </summary>
    /// <param name="defaultValue"></param>
    public void SetUserDefaultValue(string defaultValue)
    {
      UserDefaultValue = defaultValue;
      bool isValidDefaultValue = string.IsNullOrEmpty(defaultValue)
                                  || MySqlDataType.IsDateTimeOrTimeStamp && MySqlDataType.ATTRIBUTE_CURRENT_TIMESTAMP.Equals(defaultValue, StringComparison.InvariantCultureIgnoreCase)
                                  || MySqlDataType.CanStoreValue(defaultValue);

      // Update warning stating the default value is invalid for the column's data type
      if (_warnings.SetVisibility(INVALID_DEFAULT_VALUE_WARNING_KEY, !isValidDefaultValue))
      {
        OnColumnWarningsChanged();
      }
    }

    /// <summary>
    /// Sets the <see cref="DisplayName"/> property to the given display name.
    /// </summary>
    /// <param name="displayName">Display name.</param>
    /// <param name="checkForDuplicates">Flag indicating whether a check for duplicate display names should be done on this and other columns.</param>
    /// <param name="addSuffixIfDuplicate">Flag indicating if a suffix is added to the display name if an existing column with the same name is found.</param>
    public void SetDisplayName(string displayName, bool checkForDuplicates, bool addSuffixIfDuplicate = false)
    {
      if (DisplayName == displayName)
      {
        return;
      }

      // Update warning stating the column name cannot be empty
      if (_warnings.SetVisibility(EMPTY_NAME_WARNING_KEY, displayName.Length == 0))
      {
        OnColumnWarningsChanged();
      }

      DisplayName = addSuffixIfDuplicate && Table is MySqlDataTable
          ? ParentTable.GetNonDuplicateColumnName(displayName, Ordinal)
          : displayName;

      // If addSuffixIfDuplicate = true, we already made sure above to assign to this column a DisplayName that will not be a duplicate,
      // so we need to fire a duplicates check on all columns to update those warnings if needed.
      if ((addSuffixIfDuplicate || checkForDuplicates) && Table is MySqlDataTable)
      {
        ParentTable.CheckForDuplicatedColumnDisplayNames();
      }

      OnPropertyChanged("DisplayName");
    }

    /// <summary>
    /// Sets the data type of the column to an enum or set type with a valid list of values built from the column values in all rows.
    /// </summary>
    /// <param name="type">A <see cref="CollectionDataType"/>.</param>
    public void SetCollectionDataType(CollectionDataType type)
    {
      if (AutoPk)
      {
        return;
      }

      var collectionElements = new List<string>(ParentTable.Rows.Count);
      switch (type)
      {
        case CollectionDataType.Enum:
          // For the ENUM we need to consider each value as a single element, then remove the duplicates
          collectionElements.AddRange(
            ParentTable.Rows.Cast<MySqlDataRow>()
              .Select(row => string.Format("'{0}'", row[Ordinal].ToString().Replace("'", "''"))));
          break;

        case CollectionDataType.Set:
          // For the SET we need to break up each value in sub-tokens using the comma as a delimiter, then remove the duplicates.
          collectionElements.AddRange(
            ParentTable.Rows.Cast<MySqlDataRow>()
              .SelectMany(row => row[Ordinal].ToString().Replace("'", "''").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(element => string.Format("'{0}'", element))));
          break;
      }

      // Remove duplicates and sort the list for easier reading
      var firstRowElement = collectionElements.FirstOrDefault();
      collectionElements = collectionElements.Skip(1).Distinct().ToList();
      collectionElements.Sort();

      // Join the resulting list of elements into a list delimited by commas.
      var values = string.Join(",", collectionElements.ToArray());
      RowsFromSecondDataType = new MySqlDataType(string.Format("{0}({1})", type, values), true, DatesAsMySqlDates);
      if (!collectionElements.Contains(firstRowElement))
      {
        values = firstRowElement + "," + values;
      }

      RowsFromFirstDataType = new MySqlDataType(string.Format("{0}({1})", type, values), true, DatesAsMySqlDates);
      SetMySqlDataType(ParentTable.FirstRowContainsColumnNames ? MySqlDataTypeFromRowType.FromSecond : MySqlDataTypeFromRowType.FromFirst);
    }

    /// <summary>
    /// Sets the <see cref="MySqlDataType"/> property to the value of <see cref="RowsFromFirstDataType"/> or <see cref="RowsFromSecondDataType"/>.
    /// </summary>
    /// <param name="dataTypeFromRow">A <see cref="MySqlDataTypeFromRowType"/> value.</param>
    public void SetMySqlDataType(MySqlDataTypeFromRowType dataTypeFromRow)
    {
      switch (dataTypeFromRow)
      {
        case MySqlDataTypeFromRowType.FromFirst:
          SetMySqlDataType(RowsFromFirstDataType);
          break;

        case MySqlDataTypeFromRowType.FromSecond:
          SetMySqlDataType(RowsFromSecondDataType);
          break;
      }
    }

    /// <summary>
    /// Sets the given <see cref="MySqlDataType"/> to the <see cref="MySqlDataType"/> property.
    /// </summary>
    /// <param name="mySqlDataType">A <see cref="MySqlDataType"/>.</param>
    public void SetMySqlDataType(MySqlDataType mySqlDataType)
    {
      if (AutoPk)
      {
        // Always override the type if it is the AutoPK column.
        MySqlDataType = new MySqlDataType("Integer", true, DatesAsMySqlDates);
        RowsFromFirstDataType = MySqlDataType;
        RowsFromSecondDataType = MySqlDataType;
        return;
      }

      MySqlDataType = mySqlDataType ?? DefaultDataTypeForEmptyColumns;
      if (ParentTable != null)
      {
        CreateIndex = ParentTable.AutoIndexIntColumns && MySqlDataType.IsInteger;
        if (ParentTable.AutoAllowEmptyNonIndexColumns)
        {
          AllowNull = !PrimaryKey && !CreateIndex;
        }
      }

      // Reset auto increment and default values because of the data type change
      AutoIncrement = false;
      SetUserDefaultValue(null);

      OnPropertyChanged("MySqlDataType");
    }

    /// <summary>
    /// Sets the given MySQL data type to the <see cref="MySqlDataType"/> property.
    /// </summary>
    /// <param name="fullDataType">A MySQL data type as specified for new columns in a CREATE TABLE statement.</param>
    /// <param name="isValidType">Flag indicating whether the data type is a valid one, or if validations need to be performed.</param>
    /// <param name="testTypeOnData">Flag indicating if the data type will be tested against the column's data to see if the type is suitable for the data.</param>
    /// <returns><c>true</c> if the type is a valid MySQL data type, <c>false</c> otherwise.</returns>
    public bool SetMySqlDataType(string fullDataType, bool isValidType, bool testTypeOnData)
    {
      bool datesAsMySqlDates = MySqlDataType == null || MySqlDataType.DatesAsMySqlDates;
      SetMySqlDataType(new MySqlDataType(fullDataType, isValidType, datesAsMySqlDates));
      if (ParentTable != null && !ParentTable.DetectDatatype)
      {
        RowsFromFirstDataType = MySqlDataType;
        RowsFromSecondDataType = MySqlDataType;
      }

      if (AutoPk)
      {
        return true;
      }

      if (MySqlDataType == null)
      {
        return false;
      }

      if (MySqlDataType.FullType.Length == 0)
      {
        // Show warning stating the column data type cannot be empty
        if (_warnings.Show(NO_DATA_TYPE_WARNING_KEY))
        {
          OnColumnWarningsChanged();
        }

        return true;
      }

      // Hide warning stating the column data type cannot be empty
      bool warningsChanged = _warnings.Hide(NO_DATA_TYPE_WARNING_KEY);
      bool showInvalidSetOrEnumWarning = false;
      Tuple<string, string> moreInfoTuple = null;
      if (!isValidType)
      {
        showInvalidSetOrEnumWarning = MySqlDataType.InvalidSetOrEnumElementsIndexes != null && MySqlDataType.InvalidSetOrEnumElementsIndexes.Length > 0;
        if (showInvalidSetOrEnumWarning)
        {
          var invalidElementsBuilder = new StringBuilder();
          foreach (int index in MySqlDataType.InvalidSetOrEnumElementsIndexes)
          {
            invalidElementsBuilder.AppendLine(MySqlDataType.SetOrEnumElements[index]);
          }

          moreInfoTuple = new Tuple<string, string>(Resources.ColumnDataSetOrEnumNotValidMoreInfoTitle, invalidElementsBuilder.ToString());
          if (_warningsMoreInfosDictionary.ContainsKey(INVALID_SET_ENUM_WARNING_KEY))
          {
            _warningsMoreInfosDictionary[INVALID_SET_ENUM_WARNING_KEY] = moreInfoTuple;
          }
          else
          {
            _warningsMoreInfosDictionary.Add(INVALID_SET_ENUM_WARNING_KEY, moreInfoTuple);
          }
        }
      }

      // Update warning stating the column's data type is not a valid MySQL data type
      warningsChanged = _warnings.SetVisibility(INVALID_DATA_TYPE_WARNING_KEY, !MySqlDataType.IsValid) || warningsChanged;

      // Update warning stating a SET or ENUM declaration is invalid because of an error in a specific element
      warningsChanged = _warnings.SetVisibility(INVALID_SET_ENUM_WARNING_KEY, showInvalidSetOrEnumWarning) || warningsChanged;
      if (moreInfoTuple == null)
      {
        _warningsMoreInfosDictionary.Remove(INVALID_SET_ENUM_WARNING_KEY);
      }

      if (MySqlDataType.IsValid && testTypeOnData)
      {
        CanDataBeStoredInGivenColumn(this);
      }

      if (warningsChanged)
      {
        OnColumnWarningsChanged();
      }

      return MySqlDataType.IsValid;
    }

    /// <summary>
    /// Subsribes this column to the event fired when a property value in the parent table changes.
    /// </summary>
    public void SubscribeToParentTablePropertyChange()
    {
      if (ParentTable == null)
      {
        return;
      }

      ParentTable.PropertyChanged -= ParentTable_PropertyChanged;
      ParentTable.PropertyChanged += ParentTable_PropertyChanged;
    }

    /// <summary>
    /// Synchronizes this object properties copying the corresponding property values from another <see cref="MySqlDataColumn"/> object.
    /// </summary>
    /// <param name="fromColumn">The <see cref="MySqlDataColumn"/> object from which to copy property values.</param>
    public void SyncSchema(MySqlDataColumn fromColumn)
    {
      // Set first some properties that need to be set before all others because of dependencies among them.
      SetDisplayName(fromColumn.DisplayName, false);
      DataType = fromColumn.DataType;
      MySqlDataType = fromColumn.MySqlDataType.Clone() as MySqlDataType;
      RowsFromFirstDataType = fromColumn.RowsFromFirstDataType;
      RowsFromSecondDataType = fromColumn.RowsFromSecondDataType;
      AutoPk = fromColumn.AutoPk;
      InExportMode = fromColumn.InExportMode;

      // Set the rest of the properties.
      AllowNull = fromColumn.AllowNull;
      AutoIncrement = fromColumn.AutoIncrement;
      CharSet = fromColumn.CharSet;
      Collation = fromColumn.Collation;
      CreateIndex = fromColumn.CreateIndex;
      DefaultValue = fromColumn.DefaultValue;
      ExcludeColumn = fromColumn.ExcludeColumn;
      MappedDataColName = fromColumn.MappedDataColName;
      MappedDataColOrdinal = fromColumn.MappedDataColOrdinal;
      PrimaryKey = fromColumn.PrimaryKey;
      RangeColumnIndex = fromColumn.RangeColumnIndex;
      UniqueKey = fromColumn.UniqueKey;
      UserDefaultValue = fromColumn.UserDefaultValue;
    }

    /// <summary>
    /// Updates the column warning stating the advanced option to create table indexes after the data export conflicts with the Auto Increment declaration since the column must be indexed.
    /// </summary>
    public void UpdateAutoIncrementWarning()
    {
      if (_warnings.SetVisibility(TABLE_INDEXES_AFTER_EXPORT_WARNING_KEY, AutoIncrement && Settings.Default.ExportSqlQueriesCreateIndexesLast))
      {
        OnColumnWarningsChanged();
      }
    }

    /// <summary>
    /// Raises the <see cref="ColumnWarningsChanged"/> event.
    /// </summary>
    protected virtual void OnColumnWarningsChanged()
    {
      if (ColumnWarningsChanged != null)
      {
        ColumnWarningsChanged(this, new ColumnWarningsChangedArgs(this));
      }
    }

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <param name="args">Event arguments.</param>
    protected void OnPropertyChanged(PropertyChangedEventArgs args)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, args);
      }
    }

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <param name="propertyName">Name of the property whose value changed.</param>
    protected void OnPropertyChanged(string propertyName)
    {
      OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Gets a <see cref="MySqlDataType"/> that can be used to store all values in this column, doing a best match from the list of detected data types on all rows of the column.
    /// </summary>
    /// <param name="rowsDataTypesList">The list of detected data types on all rows of the column.</param>
    /// <param name="maxTextLength">The maximum length of values treated as text.</param>
    /// <returns>The consistent <see cref="MySqlDataType"/> for all values.</returns>
    private MySqlDataType GetConsistentDataTypeOnAllRows(ICollection<MySqlDataType> rowsDataTypesList, long maxTextLength)
    {
      int totalCount;
      if (rowsDataTypesList == null || (totalCount = rowsDataTypesList.Count) == 0)
      {
        return null;
      }

      if (maxTextLength <= 0)
      {
        maxTextLength = 5;
      }

      string proposedStrippedDataType = rowsDataTypesList.First().TypeName;
      string fullDataType;
      bool typesConsistent = rowsDataTypesList.All(mySqlType => mySqlType.TypeName == proposedStrippedDataType);
      if (!typesConsistent)
      {
        int integerCount;
        int decimalCount;
        typesConsistent = true;
        if (rowsDataTypesList.Count(mySqlType => mySqlType.IsChar) == totalCount)
        {
          proposedStrippedDataType = "VarChar";
        }
        else if (rowsDataTypesList.Count(mySqlType => mySqlType.IsChar || mySqlType.IsText) == totalCount)
        {
          proposedStrippedDataType = "Text";
        }
        else if ((integerCount = rowsDataTypesList.Count(mySqlType => mySqlType.TypeName == "Integer")) + rowsDataTypesList.Count(mySqlType => mySqlType.IsBool) == rowsDataTypesList.Count)
        {
          proposedStrippedDataType = "Integer";
        }
        else if (integerCount + rowsDataTypesList.Count(mySqlType => mySqlType.TypeName == "BigInt") == rowsDataTypesList.Count)
        {
          proposedStrippedDataType = "BigInt";
        }
        else if (integerCount + (decimalCount = rowsDataTypesList.Count(mySqlType => mySqlType.TypeName == "Decimal")) == rowsDataTypesList.Count)
        {
          proposedStrippedDataType = "Decimal";
        }
        else if (integerCount + decimalCount + rowsDataTypesList.Count(mySqlType => mySqlType.TypeName == "Double") == rowsDataTypesList.Count)
        {
          proposedStrippedDataType = "Double";
        }
        else if (rowsDataTypesList.Count(mySqlType => mySqlType.IsDateBased) + integerCount == rowsDataTypesList.Count)
        {
          proposedStrippedDataType = "DateTime";
        }
        else
        {
          typesConsistent = false;
        }
      }

      long maxLength = proposedStrippedDataType == "VarChar" || proposedStrippedDataType == "Decimal"
        ? rowsDataTypesList.Max(mySqlType => mySqlType.Length)
        : 0;
      if (typesConsistent)
      {
        switch (proposedStrippedDataType)
        {
          case "VarChar":
            fullDataType = string.Format("VarChar({0})", maxLength);
            break;

          case "Decimal":
            int maxDecimalPlaces = rowsDataTypesList.Max(mySqlType => mySqlType.DecimalPlaces);
            if (maxLength > 12 || maxDecimalPlaces > 2)
            {
              maxLength = 65;
              maxDecimalPlaces = 30;
            }
            else
            {
              maxLength = 12;
              maxDecimalPlaces = 2;
            }

            fullDataType = string.Format("Decimal({0}, {1})", maxLength, maxDecimalPlaces);
            break;

          default:
            fullDataType = proposedStrippedDataType;
            break;
        }
      }
      else
      {
        fullDataType = maxTextLength <= MySqlDataType.MYSQL_VARCHAR_MAX_PROPOSED_LEN
          ? string.Format("VarChar({0})", maxTextLength)
          : "Text";
      }

      return new MySqlDataType(fullDataType, true, DatesAsMySqlDates);
    }

    /// <summary>
    /// Event delegate method fired when a property value in the <see cref="ParentTable"/> changes.
    /// </summary>
    /// <param name="sender">Sender object.</param>
    /// <param name="e">Event arguments.</param>
    private void ParentTable_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case "CharSet":
          if (string.IsNullOrEmpty(CharSet))
          {
            CharSet = ParentTable.CharSet;
          }
          break;

        case "Collation":
          if (string.IsNullOrEmpty(Collation))
          {
            Collation = ParentTable.Collation;
          }
          break;
      }
    }

    /// <summary>
    /// Initializes the warnings container for this column.
    /// </summary>
    private void SetupWarnings()
    {
      _warnings = new WarningsContainer(WarningsContainer.CurrentWarningChangedMethodType.OnShowIfWarningNotPresent, 8);
      _warnings.Add(DATA_NOT_UNIQUE_WARNING_KEY, Resources.ColumnDataNotUniqueWarning);
      _warnings.Add(EMPTY_NAME_WARNING_KEY, Resources.ColumnNameRequiredWarning);
      _warnings.Add(DUPLICATE_NAME_WARNING_KEY, Resources.ColumnExistsWarning);
      _warnings.Add(NO_DATA_TYPE_WARNING_KEY, Resources.ColumnDataTypeRequiredWarning);
      _warnings.Add(INVALID_DATA_TYPE_WARNING_KEY, Resources.ColumnDataTypeNotValidWarning);
      _warnings.Add(INVALID_DEFAULT_VALUE_WARNING_KEY, Resources.ColumnDefaultValueNotValidWarning);
      _warnings.Add(INVALID_SET_ENUM_WARNING_KEY, Resources.ColumnDataSetOrEnumNotValidWarning);
      _warnings.Add(DATA_NOT_SUITABLE_APPEND_WARNING_KEY, Resources.AppendDataNotSuitableForColumnTypeWarning);
      _warnings.Add(DATA_NOT_SUITABLE_EXPORT_WARNING_KEY, Resources.ExportDataTypeNotSuitableWarning);
      _warnings.Add(TABLE_INDEXES_AFTER_EXPORT_WARNING_KEY, Resources.ExportIndexesCreatedLastWarning);
      _warningsMoreInfosDictionary = new Dictionary<string, Tuple<string, string>>(_warnings.DefinedQuantity);
    }

    /// <summary>
    /// Updates warning stating the column's data is not unique.
    /// </summary>
    private void UpdateDataUniquenessWarnings()
    {
      // Storing the value in a variable since it is easier to debug.
      var currentDuplicateGroupsCount = DuplicateGroupsFound;
      bool dataIsUnique = !_uniqueKey || CheckForDataUniqueness();
      bool duplicateGroupsCountChanged = DuplicateGroupsFound > 0 &&
                                         DuplicateGroupsFound != currentDuplicateGroupsCount;
      if (_warnings.SetVisibility(DATA_NOT_UNIQUE_WARNING_KEY, !dataIsUnique) || duplicateGroupsCountChanged)
      {
        OnColumnWarningsChanged();
      }
    }
  }
}