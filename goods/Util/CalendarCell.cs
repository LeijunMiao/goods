using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace goods
{
    public class CalendarCell : DataGridViewTextBoxCell
    {

        public CalendarCell()
            : base()
        {
            // Use the short date format.
            this.Style.Format = "d";
        }

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            CalendarEditingControl ctl =
                DataGridView.EditingControl as CalendarEditingControl;

            // ++ vic 14-aug-2009
            object val = null;
            try
            {
                val = this.Value;
            }
            catch (Exception )
            {
                // Argument ot of range (value doesn't exists in collection)
                return;
            }
            var type = val.GetType();
            if (val.GetType().Name == "String")
                val = Convert.ToDateTime(val);
            if (val != System.DBNull.Value && val != null)
                ctl.Value = (DateTime)val;
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing contol that CalendarCell uses.
                return typeof(CalendarEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains.
                return typeof(DateTime);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value.
                return DateTime.Now;
            }
        }
    }
}
