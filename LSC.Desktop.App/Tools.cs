using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSC.Desktop.App
{
    internal static class Tools
    {

        internal static void SetColumnsGrid(DataGridView grid, bool addColumns, IEnumerable<GridColumns> columns)
        {

            foreach (GridColumns col in columns)
            {

                if (addColumns)
                    grid.Columns.Add(col.Nome, col.Titulo);
                else
                    grid.Columns[col.Nome].HeaderText = col.Titulo;

            }

        }

        internal static void FormatColumnsGrid(DataGridView grid,
                                        DataGridViewContentAlignment valueAlignment = DataGridViewContentAlignment.MiddleCenter,
                                        DataGridViewContentAlignment headerAligment = DataGridViewContentAlignment.MiddleCenter,
                                        DataGridViewColumnSortMode sortMode = DataGridViewColumnSortMode.Automatic,
                                        DataGridViewAutoSizeColumnMode autoSizeMode = DataGridViewAutoSizeColumnMode.None)
        {

            grid.StandardTab = true;

            for (int n = 0; n < (grid.Columns.Count - 1); n++)
            {

                grid.Columns[n].HeaderCell.Style.Alignment = headerAligment;
                grid.Columns[n].DefaultCellStyle.Alignment = valueAlignment;
                grid.Columns[n].SortMode = sortMode;
                grid.Columns[n].AutoSizeMode = autoSizeMode;

            }

        }

        internal static bool NumberKeyPress(char key, bool allowDot, bool allowComma, bool allowNegativeNumber, bool cleanKey)
        {
            if (key.Equals('.') && allowDot) return true;
            if (key.Equals(',') && allowComma) return true;
            if (key.Equals('-') && allowNegativeNumber) return true;

            if (((int)key).Equals(8)) return true;

            if (int.TryParse(key.ToString(), out int discard)) return true;

            return false;

        }

    }

    internal struct GridColumns
    {
        public GridColumns(string titulo, string nome) : this()
        {
            Titulo = titulo;
            Nome = nome;
        }

        public string Titulo { get; set; }

        public string Nome { get; set; }
        
    }

}
