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
    public partial class frmMainForm : Form
    {
        //These variables are used to move through the two arrays below.
        int count1 = 0;
        int count2 = 0;

        /* Tracks the amount of operands entered. This helps avoid a bug in which the empty slots in the array
         * were being included in multiplications, which resulted in the answer always being zero no matter
         * what the user entered.*/
        int opTracker = 0;

        //Arrays and total variable
        double[] operands = new double[400];
        char[] operators = new char[400];
        double total = 0.0;

        public frmMainForm()
        {
            InitializeComponent();
        }

        private void frmMainForm_Load(object sender, EventArgs e)
        {
            txtResultView.Clear();
            txtSideBox.Clear();
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            txtResultView.Text += "0";
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            txtResultView.Text += "1";
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            txtResultView.Text += "2";
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            txtResultView.Text += "3";
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            txtResultView.Text += "4";
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            txtResultView.Text += "5";
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            txtResultView.Text += "6";
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            txtResultView.Text += "7";
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            txtResultView.Text += "8";
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            txtResultView.Text += "9";
        }

        private void btnDecimal_Click(object sender, EventArgs e)
        {
            txtResultView.Text += ".";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSideBox.Clear();
            txtResultView.Clear();
            resetGlobalVars();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtResultView.Text))
            {
                operands[count1] = 0.0;
                operators[count2] = '+';
                updateView("+");
                count1++;
                count2++;
                opTracker++;
            }
            else
            {
                Double.TryParse(txtResultView.Text, out operands[count1]);
                operators[count2] = '+';
                updateView("+");
                count1++;
                count2++;
                opTracker++;
            }
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtResultView.Text))
            {
                operands[count1] = 0.0;
                operators[count2] = '-';
                updateView("-");
                count1++;
                count2++;
                opTracker++;
            }
            else
            {
                Double.TryParse(txtResultView.Text, out operands[count1]);
                operators[count2] = '-';
                updateView("-");
                count1++;
                count2++;
                opTracker++;
            }
        }

        private void btnMult_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtResultView.Text))
            {
                operands[count1] = 0.0;
                operators[count2] = '*';
                updateView("*");
                count1++;
                count2++;
                opTracker++;
            }
            else
            {
                Double.TryParse(txtResultView.Text, out operands[count1]);
                operators[count2] = '*';
                updateView("*");
                count1++;
                count2++;
                opTracker++;
            }
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtResultView.Text))
            {
                operands[count1] = 0.0;
                operators[count2] = '/';
                updateView("/");
                count1++;
                count2++;
                opTracker++;
            }
            else
            {
                Double.TryParse(txtResultView.Text, out operands[count1]);
                operators[count2] = '/';
                updateView("/");
                count1++;
                count2++;
                opTracker++;
            }
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            Double.TryParse(txtResultView.Text, out operands[count1]);
            count1++;
            count2++;
            calculate();
            resetGlobalVars();
        }

        private void btnPosNeg_Click(object sender, EventArgs e)
        {
            double numberInWindow;
            double.TryParse(txtResultView.Text, out numberInWindow);
            txtResultView.Text = (numberInWindow * (-1.0)).ToString();
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            double numberInWindow;
            double.TryParse(txtResultView.Text, out numberInWindow);
            txtResultView.Text = Math.Sqrt(numberInWindow).ToString();
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (txtResultView.Text.Length < 1)
            {
                txtResultView.Clear();
            }
            else
            {
                txtResultView.Text = txtResultView.Text.Remove(txtResultView.Text.Length - 1, 1);
            }
        }

        /* Updates the view (textboxes) to reflect operators being entered.
           Takes in an operator as a param (named 'op' because 'operator' is a reserved word). */
        public void updateView(string op)
        {
            if (string.IsNullOrWhiteSpace(txtResultView.Text))
            {
                txtSideBox.Text += " 0   " + op + "   ";
            }
            else
            {
                txtSideBox.Text += " " + txtResultView.Text + "   " + op + "   ";
            }
            txtResultView.Clear();
        }

        public void calculate()
        {
            /* Add the initial value to the total; i.e.: the number the user first types in, before any operations
            are performed. */
            total += operands[0];

            // This loop starts at the 1 slot, since we already have the 0 slot from the operands array.
            try
            {
                for (int i = 1; i <= opTracker; i++)
                {
                    /* Tells the program to look at the operator that comes before the current operand
                       (operands[i]). The [i - 1] is used because we want to start looking through the
                       operators array at the 0 slot instead of the 1 slot.*/
                    switch (operators[i - 1])
                    {
                        case '+': total += operands[i]; break;
                        case '-': total -= operands[i]; break;
                        case '*': total *= operands[i]; break;
                        case '/': total /= operands[i]; break;
                    }
                }
            }
            catch(DivideByZeroException)
            {
                MessageBox.Show("DO NOT DIVIDE BY ZERO OR THE UNIVERSE WILL IMPLODE.", "STOP");
            }
            catch(ArgumentOutOfRangeException)
            {
                MessageBox.Show("You've exceeded the input limit. This calculator only allows a maximum of 400 operands and 400 operators.", "Error: input limit reached.");
            }

            txtResultView.Clear();
            txtSideBox.Clear();
            txtResultView.Text = total.ToString();
        }

        // This method returns all global variables to their initial values.
        public void resetGlobalVars()
        {
            count1 = 0;
            count2 = 0;
            total = 0.0;
            opTracker = 0;

            for (int i = 0; i < operands.Length; i++)
            {
                operands[i] = 0.0;
                operators[i] = ' ';
            }
        }
    }
}
