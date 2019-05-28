using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace events
{

    public partial class Edition : UserControl
    {
        private static Edition _instance;
        public static Edition Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Edition();
                return _instance;
            }
        }

        public Edition()
        {
            InitializeComponent();
            
            //fill the combobox with upcoming events
            foreach (KeyValuePair<string, Event.@event> kvp in Event.eventoperations.map)
                comboBox4.Items.Add((kvp.Value.getName() + " " + kvp.Key));
           
            DateTime systime = DateTime.Today;

            dateTimePicker1.MinDate = systime.Date;
            textBox7.Text = textBox8.Text = dateTimePicker1.Text = dateTimePicker2.Text = dateTimePicker3.Text = dateTimePicker4.Text = "";


        }


        //updating an event
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            SoundPlayer sou = new SoundPlayer("Operations.WAV");
            sou.Play();
            int ind; int index;
            string[] arr = new string[2];
            
            //split the selected event to check with it
            arr = comboBox4.SelectedItem.ToString().Split(' ');

            DialogResult r = MessageBox.Show("Do You Really Want to Update " + arr[0] + " Event ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                //first case , if the user doesn't change the start date or the end date
                if (Event.eventoperations.map[arr[1]].getSdate() == dateTimePicker1.Text && Event.eventoperations.map[arr[1]].getEdate() == dateTimePicker2.Text)
                {
                    //updating event details without any dates

                    Event.eventoperations.map[arr[1]].setName(textBox7.Text);
                    Event.eventoperations.map[arr[1]].setPlace(textBox8.Text);
                    string[] tt = new string[2];
                    char c = ' ';
                    tt = dateTimePicker3.Text.Split(c);
                    string time;
                    if (tt[1] == "م")
                        time = tt[0] + " " + "PM";
                    else
                        time = tt[0] + " " + "AM";
                    Event.eventoperations.map[arr[1]].setStime(time);
                    Event.eventoperations.map[arr[1]].setremind(dateTimePicker4.Text);

                    textBox7.Text = dateTimePicker1.Text = dateTimePicker2.Text = dateTimePicker3.Text = textBox8.Text = dateTimePicker4.Text = "";
                    MessageBox.Show("Updated", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }



                else
                {
                    //if the user changed the start date or the end date or both 

                    //delete the wanted event temporary to check if it doesn't intersect with any other events



                    Event.@event ev = new Event.@event();
                    ev = Event.eventoperations.map[arr[1]];
                    ind = Event.eventoperations.map.IndexOfKey(arr[1]);
                    Event.eventoperations.tmp.RemoveAt(ind);
                    Event.eventoperations.map.Remove(arr[1]);
                    string[] edate = new string[3];
                    string[] sdate = new string[3];
                    int y1, m1, d1, y2, m2, d2;
                    bool valid_event = true;
                    string time;
                    string[] tt = new string[2];
                    char c = ' ';
                    tt = dateTimePicker3.Text.Split(c);

                    if (tt[1] == "م")
                        time = tt[0] + " " + "PM";
                    else
                        time = tt[0] + " " + "AM";


                    Event.@event eva = new Event.@event(textBox7.Text, dateTimePicker1.Text, dateTimePicker2.Text, textBox8.Text, time, dateTimePicker4.Text);

                    //first case: there is an event with the same start date
                    if (Event.eventoperations.map.ContainsKey(dateTimePicker1.Text))
                    {
                        MessageBox.Show("There is an event on " + dateTimePicker1.Text, "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    else
                    {
                        Event.eventoperations.map.Add(dateTimePicker1.Text, eva);
                        index = Event.eventoperations.map.IndexOfKey(dateTimePicker1.Text);

                        //second case: it is the first event to add
                        if (Event.eventoperations.map.Count == 1)
                            valid_event = true;

                            //third case: the new updated event became the first
                        else if (index == 0 && Event.eventoperations.map.Count > 1)
                        {

                            edate = eva.getEdate().Split('/');
                            sdate = Event.eventoperations.tmp[index].start_date.Split('/');
                            y1 = Convert.ToInt16(edate[0]);
                            m1 = Convert.ToInt16(edate[1]);
                            d1 = Convert.ToInt16(edate[2]);
                            y2 = Convert.ToInt16(sdate[0]);
                            m2 = Convert.ToInt16(sdate[1]);
                            d2 = Convert.ToInt16(sdate[2]);

                            //checking if the end date of the new updated event doesn't intersect with the next event
                            if ((y1 > y2) || (y1 == y2 && m1 > m2) || (y1 == y2 && m1 == m2 && d1 >= d2))
                                valid_event = false;

                        }
                        //fourth case: the new updated event became the last
                        else if (index == Event.eventoperations.map.Count - 1 && Event.eventoperations.map.Count > 1)
                        {
                            sdate = eva.getSdate().Split('/');

                            edate = Event.eventoperations.tmp[index - 1].end_date.Split('/');
                            y1 = Convert.ToInt16(sdate[0]);
                            m1 = Convert.ToInt16(sdate[1]);
                            d1 = Convert.ToInt16(sdate[2]);
                            y2 = Convert.ToInt16(edate[0]);
                            m2 = Convert.ToInt16(edate[1]);
                            d2 = Convert.ToInt16(edate[2]);

                            //checking if the start date of the new updated event doesn't intersect with the previous event

                            if ((y1 < y2) || (y1 == y2 && m1 < m2) || (y1 == y2 && m1 == m2 && d1 <= d2))
                                valid_event = false;
                        }
                        //fifth case: the new updated event became between two or several events
                        else
                        {

                            sdate = eva.getSdate().Split('/');

                            edate = Event.eventoperations.tmp[index - 1].end_date.Split('/');
                            y1 = Convert.ToInt16(sdate[0]);
                            m1 = Convert.ToInt16(sdate[1]);
                            d1 = Convert.ToInt16(sdate[2]);
                            y2 = Convert.ToInt16(edate[0]);
                            m2 = Convert.ToInt16(edate[1]);
                            d2 = Convert.ToInt16(edate[2]);

                            //checking if the start date of the new updated not intersects with the previous end date
                            if ((y1 < y2) || (y1 == y2 && m1 < m2) || (y1 == y2 && m1 == m2 && d1 <= d2))
                                valid_event = false;

                            else if (valid_event == true)
                            {
                                edate = eva.getEdate().Split('/');
                                sdate = Event.eventoperations.tmp[index].start_date.Split('/');
                                y1 = Convert.ToInt16(edate[0]);
                                m1 = Convert.ToInt16(edate[1]);
                                d1 = Convert.ToInt16(edate[2]);
                                y2 = Convert.ToInt16(sdate[0]);
                                m2 = Convert.ToInt16(sdate[1]);
                                d2 = Convert.ToInt16(sdate[2]);

                                //checking if the end date of the new updated not intersects with the next start date
                                if ((y1 > y2) || (y1 == y2 && m1 > m2) || (y1 == y2 && m1 == m2 && d1 >= d2))
                                    valid_event = false;
                            }
                        }
                        //checking if the new updated event doesn't intersect with any other event
                        if (valid_event == true)
                        {
                            Event.temp t = new Event.temp();
                            t.start_date = eva.getSdate();
                            t.end_date = eva.getEdate();
                            Event.eventoperations.tmp.Insert(index, t);
                            textBox7.Text = dateTimePicker1.Text = dateTimePicker2.Text = dateTimePicker3.Text = textBox8.Text = dateTimePicker4.Text = "";
                            MessageBox.Show("Updated", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            //when updating is faild and found the new updated intersects with other ,
                            //removing the new added and restore the last virsion
                            Event.eventoperations.map.RemoveAt(index);
                            MessageBox.Show("There is an intersection with another Event", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Event.eventoperations.map.Add(ev.getSdate(), ev);
                            Event.temp tm = new Event.temp();
                            tm.start_date = ev.getSdate();
                            tm.end_date = ev.getEdate();
                            Event.eventoperations.tmp.Insert(ind, tm);
                        }

                    }


                }
            }
        }


        //removing an event
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            SoundPlayer sou = new SoundPlayer("Operations.WAV");
            sou.Play();
            string[] arr = new string[2];
            arr = comboBox4.SelectedItem.ToString().Split(' ');
            DialogResult r = MessageBox.Show("Do You Really Want to Remove " + arr[0] + " Event ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                Event.eventoperations.tmp.RemoveAt(Event.eventoperations.map.IndexOfKey(arr[1]));
                Event.eventoperations.map.Remove(arr[1]);
                comboBox4.Items.Remove(comboBox4.Text);
                textBox7.Text = textBox8.Text = dateTimePicker1.Text = dateTimePicker2.Text = dateTimePicker3.Text = dateTimePicker4.Text = "";
            }
        }



        //spliting the attributes to text boxes and date time pickers
        private void comboBox4_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string[] arr = new string[2];
            arr = comboBox4.SelectedItem.ToString().Split(' ');
            textBox7.Text = arr[0];
            textBox8.Text = Event.eventoperations.map[arr[1]].getPlace();
            dateTimePicker1.Text = arr[1];
            dateTimePicker2.Text = Event.eventoperations.map[arr[1]].getEdate();
            dateTimePicker3.Text = Event.eventoperations.map[arr[1]].getStime();
            dateTimePicker4.Text = Event.eventoperations.map[arr[1]].getremind();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value;
            dateTimePicker4.MaxDate = dateTimePicker1.Value;
        }
    }
}
