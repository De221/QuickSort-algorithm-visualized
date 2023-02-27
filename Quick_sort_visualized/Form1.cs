using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quick_sort_visualized
{
    public partial class Form1 : Form
    {
        //51,14,95,66,72,52,38,39,-7,41,15 примерни числа за тестване
        //51,14,95,66,72,52,38,39,-7,41,15,0,-55,8,2,0,666
        private ManualResetEvent pause;

        private int counter = -1;
        private int rectanglesCount = 0;
        private int labelCount = 1;
        private int pivotCount = 0;

        #region QuickSort
        private void QuickSort(Rectangle[] arr, List<SwapArc> swaparcs, int left, int right)
        {
            //Създава нужните лейбъли
            Label[] labels = new Label[6];
            for (int i = 0; i < 6; i++)
            {
                Invoke(new MethodInvoker(delegate
                {
                    labels[i] = new Label
                    {
                        Name = "label" + labelCount++,
                        Visible = false,
                        BackColor = Color.Transparent,
                        AutoSize = true
                    };
                    Controls.Add(labels[i]);
                }));     
            }

            if (left < right)
            {
                pivotCount++;
                int pivot = Partition(arr, swaparcs, left, right, labels);               

                if (pivot > 1)
                {
                    new Thread(() => QuickSort(arr, swaparcs, left, pivot - 1)).Start();
                }
                if (pivot + 1 < right)
                {
                    new Thread(() => QuickSort(arr, swaparcs, pivot + 1, right)).Start();
                }
            }
        }

        private int Partition(Rectangle[] arr, List<SwapArc> swaparcs, int left, int right, Label[] labels)
        {
            int initial_left = left;
            int initial_right = right + 1;
            Point left_location = new Point(0,0);
            Point right_location = new Point(0,0);
            bool leftFlag = false;
            int pivot_index = left;
            arr[left] = new Rectangle(arr[left].location, arr[left].width, Color.Yellow, arr[left].number);
            Point pivot_location = new Point(arr[left].location.X + arr[left].width / 2 - 15, arr[left].location.Y - 15);
            left++;
            Invoke(new MethodInvoker(delegate { labels[0].Text = "pivot" + pivotCount; labels[0].Visible = true; labels[0].Location = new Point(pivot_location.X, pivot_location.Y); }));
            Invalidate();
            pause.WaitOne();
            pause.Reset();

            if(left != right)
            {
                left_location = new Point(arr[left].location.X + arr[left].width / 2 - 10, arr[left].location.Y - 15);
                Invoke(new MethodInvoker(delegate { labels[1].Text = "left"; labels[1].Visible = true; labels[1].Location = new Point(left_location.X, left_location.Y); }));

                right_location = new Point(arr[right].location.X + arr[right].width / 2 - 15, arr[right].location.Y - 15);
                Invoke(new MethodInvoker(delegate { labels[2].Text = "right"; labels[2].Visible = true; labels[2].Location = new Point(right_location.X, right_location.Y); }));

                Invalidate();
                pause.WaitOne();
                pause.Reset();
            }
            else
            {
                right_location = new Point(arr[right].location.X + arr[right].width / 2 - 15, arr[right].location.Y - 15);
                Invoke(new MethodInvoker(delegate { labels[2].Text = "l≮r"; labels[2].Visible = true; labels[2].Location = new Point(right_location.X, right_location.Y); }));
                if (arr[pivot_index].number > arr[right].number)
                {
                    Invoke(new MethodInvoker(delegate { labels[4].Text = "< pivot"; labels[4].Visible = true; labels[4].Location = new Point(right_location.X - 5, right_location.Y + 45); }));                   
                    arr[right] = new Rectangle(arr[right].location, arr[right].width, Color.Red, arr[right].number);
                }
                if (arr[pivot_index].number <= arr[right].number)
                {
                    Invoke(new MethodInvoker(delegate { labels[4].Text = "> pivot"; labels[4].Visible = true; labels[4].Location = new Point(right_location.X - 5, right_location.Y + 45); }));                   
                    arr[right] = new Rectangle(arr[right].location, arr[right].width, Color.LightGreen, arr[right].number);
                }

                Invalidate();
                pause.WaitOne();
                pause.Reset();
            }

            while (true)
            {
                while (arr[right].number >= arr[pivot_index].number && left != right)
                {
                    if (left < right)
                    {                       
                        //Проверка дали елемента си е на правилно място(ако да - светва в зелено)
                        if (arr[right].color != Color.LightGreen)
                        {
                            Point previous_right_location = new Point(arr[right].location.X + arr[right].width / 2 - 15, arr[right].location.Y - 15);
                            Invoke(new MethodInvoker(delegate { labels[4].Text = "> pivot"; labels[4].Visible = true; labels[4].Location = new Point(previous_right_location.X - 5, previous_right_location.Y + 45); }));
                            Invalidate();
                            pause.WaitOne();
                            pause.Reset();

                            arr[right] = new Rectangle(arr[right].location, arr[right].width, Color.LightGreen, arr[right].number);
                            Invalidate();
                            pause.WaitOne();
                            pause.Reset();
                        }

                        if (left == right - 1)
                        {
                            //Проверява дали е стигнато до посления елемент(за смяна с пивота)
                            leftFlag = true;
                            if(arr[pivot_index].number > arr[left].number)
                            {
                                Invoke(new MethodInvoker(delegate { labels[1].Visible = false; }));
                                Invoke(new MethodInvoker(delegate { labels[2].Text = "l≮r"; labels[2].Visible = true; labels[2].Location = new Point(left_location.X, left_location.Y); }));
                                Invoke(new MethodInvoker(delegate { labels[4].Text = "< pivot"; labels[4].Visible = true; labels[4].Location = new Point(left_location.X - 5, left_location.Y + 45); }));
                                arr[left] = new Rectangle(arr[left].location, arr[left].width, Color.Red, arr[left].number);
                            }
                            else
                            {
                                Invoke(new MethodInvoker(delegate { labels[1].Visible = false; }));
                                Invoke(new MethodInvoker(delegate { labels[2].Text = "l≮r"; labels[2].Visible = true; labels[2].Location = new Point(left_location.X, left_location.Y); }));
                                Invoke(new MethodInvoker(delegate { labels[4].Text = "> pivot"; labels[4].Visible = true; labels[4].Location = new Point(left_location.X - 5, left_location.Y + 45); }));
                                arr[left] = new Rectangle(arr[left].location, arr[left].width, Color.LightGreen, arr[left].number);
                            }                          
                        }
                        else
                        {
                            //Премества показателя на следващия елемент
                            right_location = new Point(arr[right - 1].location.X + arr[right - 1].width / 2 - 15, arr[right - 1].location.Y - 15);
                            Invoke(new MethodInvoker(delegate { labels[2].Text = "right"; labels[2].Visible = true; labels[2].Location = new Point(right_location.X, right_location.Y); }));
                        }                       

                        //Цвят: готова проверка
                        arr[right] = new Rectangle(arr[right].location, arr[right].width, Color.Azure, arr[right].number);
                        Invoke(new MethodInvoker(delegate { labels[3].Visible = false; }));

                        Invalidate();
                        pause.WaitOne();
                        pause.Reset();
                        right--;
                    }
                    else
                    {
                        right--;
                        break;
                    }
                }

                while (arr[left].number <= arr[pivot_index].number && left != right)
                {
                    if (left < right)
                    {
                        //Проверка дали елемента си е на правилно място(ако да - светва в зелено)
                        if(arr[left].color != Color.LightGreen)
                        {
                            Point previous_left_location = new Point(arr[left].location.X + arr[left].width / 2 - 15, arr[left].location.Y - 15);
                            Invoke(new MethodInvoker(delegate { labels[4].Text = "< pivot"; labels[4].Visible = true; labels[4].Location = new Point(previous_left_location.X - 5, previous_left_location.Y + 45); }));
                            Invalidate();
                            pause.WaitOne();
                            pause.Reset();

                            arr[left] = new Rectangle(arr[left].location, arr[left].width, Color.LightGreen, arr[left].number);
                            Invalidate();
                            pause.WaitOne();
                            pause.Reset();
                        }

                        if (left == right-1)
                        {
                            //Проверява дали е стигнато до посления елемент(за смяна с пивота)
                            if (arr[pivot_index].number > arr[right].number)
                            {
                                Invoke(new MethodInvoker(delegate { labels[1].Visible = false; }));
                                Invoke(new MethodInvoker(delegate { labels[2].Text = "l≮r"; labels[2].Visible = true; labels[2].Location = new Point(right_location.X, right_location.Y); }));
                                Invoke(new MethodInvoker(delegate { labels[3].Text = "< pivot"; labels[3].Visible = true; labels[3].Location = new Point(right_location.X - 5, right_location.Y + 45); }));
                                arr[right] = new Rectangle(arr[right].location, arr[right].width, Color.Red, arr[right].number);
                            }
                            else
                            {
                                Invoke(new MethodInvoker(delegate { labels[1].Visible = false; }));
                                Invoke(new MethodInvoker(delegate { labels[2].Text = "l≮r"; labels[2].Visible = true; labels[2].Location = new Point(right_location.X, right_location.Y); }));
                                Invoke(new MethodInvoker(delegate { labels[3].Text = "> pivot"; labels[3].Visible = true; labels[3].Location = new Point(right_location.X - 5, right_location.Y + 45); }));
                                arr[right] = new Rectangle(arr[right].location, arr[right].width, Color.LightGreen, arr[right].number);
                            }                            
                        }
                        else
                        {
                            //Премества показателя на следващия елемент
                            left_location = new Point(arr[left + 1].location.X + arr[left + 1].width / 2 - 10, arr[left + 1].location.Y - 15);
                            Invoke(new MethodInvoker(delegate { labels[1].Text = "left"; labels[1].Visible = true; labels[1].Location = new Point(left_location.X, left_location.Y); }));
                        }

                        //Цвят: готова проверка
                        arr[left] = new Rectangle(arr[left].location, arr[left].width, Color.Azure, arr[left].number);
                        Invoke(new MethodInvoker(delegate { labels[4].Visible = false; }));

                        Invalidate();
                        pause.WaitOne();
                        pause.Reset();
                        left++;
                    }
                    else
                    {
                        left++;
                        break;
                    }
                }


                if (left < right)
                {
                    //Размяна на елементи(Прави ги червени)
                    Invoke(new MethodInvoker(delegate { labels[4].Text = "> pivot"; labels[4].Visible = true; labels[4].Location = new Point(left_location.X - 5, left_location.Y + 45); }));
                    Invalidate();
                    pause.WaitOne();
                    pause.Reset();
                    arr[left] = new Rectangle(arr[left].location, arr[left].width, Color.Red, arr[left].number);
                    Invalidate();
                    pause.WaitOne();
                    pause.Reset();

                    Invoke(new MethodInvoker(delegate { labels[3].Text = "< pivot"; labels[3].Visible = true; labels[3].Location = new Point(right_location.X - 5, right_location.Y + 45); }));
                    Invalidate();
                    pause.WaitOne();
                    pause.Reset();
                    arr[right] = new Rectangle(arr[right].location, arr[right].width, Color.Red, arr[right].number);
                    Invalidate();
                    pause.WaitOne();
                    pause.Reset();

                    Invoke(new MethodInvoker(delegate { labels[5].Text = "swap"; labels[5].Visible = true; labels[5].Location = new Point((arr[right].location.X - arr[left].location.X) / 2 + arr[left].location.X, arr[left].location.Y - 50); }));
                    swaparcs.Add(new SwapArc(new Point(arr[left].location.X + arr[left].width / 2, left_location.Y), new Point(arr[right].location.X + arr[right].width / 2, right_location.Y)));

                    Invalidate();
                    pause.WaitOne();
                    pause.Reset();

                    swaparcs.RemoveAt(swaparcs.Count - 1);
                    Invoke(new MethodInvoker(delegate { labels[5].Visible = false; labels[3].Visible = false; labels[4].Visible = false; }));
                    int temp = arr[left].number;
                    arr[left] = new Rectangle(arr[left].location, arr[left].width, Color.LightGreen, arr[right].number);
                    arr[right] = new Rectangle(arr[right].location, arr[right].width, Color.LightGreen, temp);
                    Invalidate();
                    pause.WaitOne();
                    pause.Reset();
                }
                else
                {
                    //Размяна на последния елемент с пивота                  
                    if (leftFlag)
                    {
                        if (arr[left].color == Color.Red)
                        {
                            Invoke(new MethodInvoker(delegate {
                                labels[5].Text = "swap"; labels[5].Visible = true; labels[5].Location = new Point((arr[left].location.X - arr[pivot_index].location.X) / 2 + arr[pivot_index].location.X, arr[left].location.Y - 50); }));
                            swaparcs.Add(new SwapArc(new Point(arr[pivot_index].location.X + arr[pivot_index].width / 2, arr[pivot_index].location.Y - 15),
                                new Point(arr[left].location.X + arr[left].width / 2, arr[left].location.Y - 15)));
                            Invalidate();
                            pause.WaitOne();
                            pause.Reset();

                            swaparcs.RemoveAt(swaparcs.Count - 1);

                            int temp = arr[pivot_index].number;
                            Color tempColor = arr[pivot_index].color;
                            arr[pivot_index] = new Rectangle(arr[pivot_index].location, arr[pivot_index].width, Color.Azure, arr[left].number);
                            arr[left] = new Rectangle(arr[left].location, arr[left].width, tempColor, temp);
                            Invoke(new MethodInvoker(delegate { labels[0].Location = new Point(arr[left].location.X + arr[left].width / 2 - 15, arr[left].location.Y - 15); }));
                            Invoke(new MethodInvoker(delegate { labels[1].Visible = false; labels[2].Visible = false; labels[3].Visible = false; labels[4].Visible = false; labels[5].Visible = false; }));
                            Invalidate();
                            Invoke(new MethodInvoker(delegate { labels[0].Visible = false; }));
                            arr[pivot_index] = new Rectangle(arr[pivot_index].location, arr[pivot_index].width, Color.Azure, arr[pivot_index].number);

                            Coloring(arr, initial_left, initial_right, left);

                            return left;//left
                        }
                        if (arr[left].color == Color.LightGreen)
                        {
                            Invoke(new MethodInvoker(delegate { labels[0].Visible = false; labels[1].Visible = false; labels[2].Visible = false; labels[3].Visible = false; labels[4].Visible = false; labels[5].Visible = false; }));
                            
                            Coloring(arr, initial_left, initial_right, pivot_index);

                            return pivot_index;
                        }
                    }
                    else if (!leftFlag)
                    {
                        if (arr[right].color == Color.Red)
                        {
                            Invoke(new MethodInvoker(delegate {
                                labels[5].Text = "swap"; labels[5].Visible = true; labels[5].Location = new Point((arr[right].location.X - arr[pivot_index].location.X) / 2 + arr[pivot_index].location.X, arr[right].location.Y - 50); }));
                            swaparcs.Add(new SwapArc(new Point(arr[pivot_index].location.X + arr[pivot_index].width / 2, arr[pivot_index].location.Y - 15),
                                new Point(arr[right].location.X + arr[right].width / 2, arr[right].location.Y - 15)));
                            Invalidate();
                            pause.WaitOne();
                            pause.Reset();

                            swaparcs.RemoveAt(swaparcs.Count - 1);

                            int temp = arr[pivot_index].number;
                            Color tempColor = arr[pivot_index].color;
                            arr[pivot_index] = new Rectangle(arr[pivot_index].location, arr[pivot_index].width, Color.Azure, arr[right].number);
                            arr[right] = new Rectangle(arr[right].location, arr[right].width, tempColor, temp);
                            Invoke(new MethodInvoker(delegate { labels[0].Location = new Point(arr[right].location.X + arr[right].width / 2 - 15, arr[right].location.Y - 15); }));
                            Invoke(new MethodInvoker(delegate { labels[1].Visible = false; labels[2].Visible = false; labels[3].Visible = false; labels[4].Visible = false; labels[5].Visible = false; }));
                            Invalidate();
                            Invoke(new MethodInvoker(delegate { labels[0].Visible = false; }));
                            arr[pivot_index] = new Rectangle(arr[pivot_index].location, arr[pivot_index].width, Color.Azure, arr[pivot_index].number);

                            Coloring(arr, initial_left, initial_right, right);

                            return right;//right
                        }
                        if (arr[right].color == Color.LightGreen)
                        {
                            Invoke(new MethodInvoker(delegate { labels[0].Visible = false; labels[1].Visible = false; labels[2].Visible = false; labels[3].Visible = false; labels[4].Visible = false; labels[5].Visible = false; }));
                            
                            Coloring(arr, initial_left, initial_right, pivot_index);

                            return pivot_index;
                        }
                    }
                }
            }
        }
        private void Coloring(Rectangle[] arr, int left, int right, int pivot_index)
        {
            //Връща цвета на всички квадрати към прозрачен и оцветява пивота в лилаво преди да започне отново
            for (int i = left; i < right; i++)
            {
                if (i == pivot_index)
                    arr[i] = new Rectangle(arr[i].location, arr[i].width, Color.Purple, arr[i].number);
                else
                    arr[i] = new Rectangle(arr[i].location, arr[i].width, Color.FromArgb(0, 255, 255, 255), arr[i].number);
            }
            for (int i = left; i < right; i++) //Оцветява единичните останали блокове в лилаво
            {
                if (i - 1 >= 0 && i + 1 <= arr.Length - 1)
                    if (arr[i - 1].color == Color.Purple && arr[i + 1].color == Color.Purple)
                        arr[i] = new Rectangle(arr[i].location, arr[i].width, Color.Purple, arr[i].number);
                if (i == left && arr[left + 1].color == Color.Purple)
                    arr[i] = new Rectangle(arr[i].location, arr[i].width, Color.Purple, arr[i].number);
                if (i == arr.Length - 1 && arr[arr.Length - 2].color == Color.Purple)
                    arr[i] = new Rectangle(arr[i].location, arr[i].width, Color.Purple, arr[i].number);
            }
            Invalidate();
        }
        #endregion      
        public Form1()
        {
            InitializeComponent();

            pause = new ManualResetEvent(false);
        }       

        private List<Rectangle> _rectangles = new List<Rectangle>();
        private Rectangle[] _rectanglesArr = { };
        private List<SwapArc> _swaparcs = new List<SwapArc>();
        List<int> numbers_list = new List<int>();
        int[] numbers_array = { };
        int _width;
        bool program_finished = false;
        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (var rectangle in _rectanglesArr)
                rectangle.Paint(e.Graphics);
            foreach (var swaparc in _swaparcs)
                swaparc.Paint(e.Graphics);
        }

        private void startButton_Click(object sender, EventArgs e)
        {            
            if (counter == -1)
            {
                bool is_valid = true;
                string text = arrayBox.Text;
                string[] text_array = text.Split(',');
                foreach (string s in text_array)
                {
                    int temp;
                    if (int.TryParse(s, out temp) != true)
                        is_valid = false;
                    if (is_valid)
                        numbers_list.Add(temp);
                }
                if (numbers_list.Count == 1)
                {
                    numbers_list.Clear();
                    label0.Visible = true;
                    label0.Text = "Input more than one number!";
                }
                else if (is_valid == false)
                {
                    label0.Visible = true;
                    label0.Text = "Input only numbers divided by commas!";
                    numbers_list.Clear();
                }                
                else 
                    counter++;
            }            
            if (counter == 0)
            {
                Point point1 = new Point(20, 66);
                numbers_array = numbers_list.ToArray();
                if (numbers_array.Length >= 25)
                    numbers_array = numbers_list.Take(25).ToArray();
                rectanglesCount = numbers_array.Length;

                arrayBox.Visible = false;
                label0.Visible = false;
                startButton.Text = "Next";
                _width = (500 - 5 * rectanglesCount) / rectanglesCount;
                for (int i = 0; i < rectanglesCount; i++)
                {
                    _rectangles.Add(new Rectangle(point1, _width, Color.Transparent, numbers_array[i]));
                    point1.Offset(_width + 5, 0);
                    if (i == rectanglesCount - 1)
                        _rectanglesArr = _rectangles.ToArray();
                }
                counter++;
                Invalidate();
            }
            else if (counter == 1)
            {
                Thread t = new Thread(() => QuickSort(_rectanglesArr, _swaparcs, 0, _rectangles.Count - 1));
                t.Start();
                counter++;
            }
            else if (counter == 2)
            {
                pause.Set();
                program_finished = true;
                foreach (Rectangle r in _rectanglesArr)
                    if (r.color != Color.Purple)
                        program_finished = false;
            }       
            if(program_finished)
            {
                label0.Text = "Sorted successfully!";
                label0.Visible = true;
                label0.Location = new Point(180, 160);
                startButton.Text = "Quit";
                counter++;
            }
            if (counter == 4)
                this.Close();
        }
    }
}
