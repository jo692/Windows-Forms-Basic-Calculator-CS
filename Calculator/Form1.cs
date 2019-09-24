using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        double resultValue = 0;             //Stores the values inputted prior to an operator click
        String operationPerformed = "";     
        bool isOperationPerformed = false;  //Signals if the last button clicked was an operator

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            //Check if only the default zero is in the display or if the last press was an operation, if so clear
            if(calcDisplay.Text == "0" || isOperationPerformed)
            {
                calcDisplay.Clear();
            }

            //Cast sender to a Button so we can access the button text
            Button button = (Button)sender;

            //If the decimal button is pressed and there is already one in the current display, do nothing
            if(button.Text == "." && calcDisplay.Text.Contains("."))
            {
                return;
            }
            else
            {
                calcDisplay.Text += button.Text;
                isOperationPerformed = false;
            }
        }

        //Clicking an operator will update the label with the current calculation
        private void OperatorClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            //Carry out multiple operations in one sum (i.e: 5+5+10+4...)
            if(resultValue != 0)
            {
                equalsButton.PerformClick();
                operationPerformed = button.Text;
                isOperationPerformed = true;
                labelCurrentCalculation.Text = (resultValue).ToString() + " " + operationPerformed;
            }
            else
            {
                operationPerformed = button.Text;
                resultValue = Double.Parse(calcDisplay.Text);   //Take the numbers currently in the display and store in resultValue
                isOperationPerformed = true;        //Let program know the last button press was an operator
                labelCurrentCalculation.Text = (resultValue).ToString() + " " + operationPerformed; //Update label with new calculation
            }
        }

        //ClearEntry will simply clear the current input
        private void ClearEntryClick(object sender, EventArgs e)
        {
            calcDisplay.Text = "0";
        }

        //Clear will clear the current entry and any previous inputs prior to an operator
        private void ClearClick(object sender, EventArgs e)
        {
            calcDisplay.Text = "0";
            resultValue = 0;
            isOperationPerformed = false;
            labelCurrentCalculation.Text = "";
        }

        //Equals will perform the calculation inputted so far, display the answer and clear the label
        private void EqualsClick(object sender, EventArgs e)
        {
            switch(operationPerformed)
            {
                case "+":
                    calcDisplay.Text = (resultValue + Double.Parse(calcDisplay.Text)).ToString();
                    break;
                case "-":
                    calcDisplay.Text = (resultValue - Double.Parse(calcDisplay.Text)).ToString();
                    break;
                case "*":
                    calcDisplay.Text = (resultValue * Double.Parse(calcDisplay.Text)).ToString();
                    break;
                case "/":
                    calcDisplay.Text = (resultValue / Double.Parse(calcDisplay.Text)).ToString();
                    break;
                default:
                    break;
            }
            resultValue = Double.Parse(calcDisplay.Text);   //Update result value, this is used for multiple operations
            labelCurrentCalculation.Text = "";
        }
    }
}
