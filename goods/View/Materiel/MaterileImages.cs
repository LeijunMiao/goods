using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using goods.Controller;
using goods.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace goods
{
    public partial class MaterileImages : Form
    {
        int mid = -1;
        string image = "";
        int millisecond = 300000;
        utilCls util = new utilCls();
        materielCtrl ctrl = new materielCtrl();
        public MaterileImages(int id)
        {
            InitializeComponent();
            mid = id;
            initPage();
            loadData(true);
        }
        private void initPage()
        {
            contextMenuStrip1.Opening += ContextMenuStrip1_Opening;
            openFileDialog1.Filter = "All Image Files|*.bmp;*.ico;*.gif;*.jpeg;*.jpg;*.png;*.tif;*.tiff|" +
                "Windows Bitmap(*.bmp)|*.bmp|" +
                "Windows Icon(*.ico)|*.ico|" +
                "Graphics Interchange Format (*.gif)|(*.gif)|" +
                "JPEG File Interchange Format (*.jpg)|*.jpg;*.jpeg|" +
                "Portable Network Graphics (*.png)|*.png|" +
                "Tag Image File Format (*.tif)|*.tif;*.tiff";
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void loadData(bool isFirst)
        {
            listView1.Clear();
            imageList1.Images.Clear();
            DataTable dt = ctrl.getImages(mid);
            var j = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Stream s = util.Info("ftp://" + ServerInfo.ServerIP + "/", dt.Rows[i]["name"].ToString());
                ListViewItem li = new ListViewItem();
                li.Text = dt.Rows[i]["name"].ToString();
                if (s == null)
                {
                    listView1.Items.Add(li);
                    continue;
                }
                else
                {
                    Image img = Image.FromStream(s);
                    imageList1.Images.Add(img);
                    li.ImageIndex = j;         //对应指定即可
                    j++;
                    listView1.Items.Add(li);
                }
            }
            if (dt.Rows.Count != imageList1.Images.Count && isFirst)
            {
                MessageBox.Show("部分图片在服务器上丢失！");
            }
            imageList1.ImageSize = new Size(120, 80);
            this.listView1.LargeImageList = imageList1;
            if (listView1.Items.Count > 0) listView1.Items[0].Selected = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                image = openFileDialog1.FileName;
                pictureBox2.Image = Image.FromFile(image);
            }
        }

        private void reset()
        {
            image = "";
            lblTime.Text = "";
            lblSize.Text = "";
            lblSpeed.Text = "";
            lblState.Text = "";
            progressBar1.Value = 0;
            openFileDialog1.Reset();
            pictureBox2.Image = null;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (image != "")
            {
                string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + DateTime.Now.Millisecond.ToString();
                //获取图片的扩展名
                string Extent = System.IO.Path.GetExtension(openFileDialog1.FileName);
                //重命名图片
                fileName += Extent;
                uploadmodel res = Upload_Request2("http://" + ServerInfo.ServerIP + ":7070/api/materiel/image", image, fileName, progressBar1);
                if (res.code == 0)
                {
                    MessageModel m = ctrl.addImage(new ImageModel(fileName, res.url, mid));
                    if (m.Code == 0)
                    {
                        lblState.Text = "已完成";
                        progressBar1.Value = int.MaxValue;
                        MessageBox.Show("保存成功！");
                        reset();
                        loadData(false);
                    }
                }
                else
                {
                    MessageBox.Show(res.msg);
                }
            }
            else
            {
                MessageBox.Show("请先选择图片。");
            }
        }

        /// <summary>   
        /// 将本地文件上传到指定的服务器(HttpWebRequest方法)   
        /// </summary>   
        /// <param name="address">文件上传到的服务器</param>   
        /// <param name="fileNamePath">要上传的本地文件（全路径）</param>   
        /// <param name="saveName">文件上传后的名称</param>   
        /// <param name="progressBar">上传进度条</param>   
        /// <returns>成功返回0，失败返回1</returns>   
        private uploadmodel Upload_Request2(string address, string fileNamePath, string saveName, ProgressBar progressBar)
        {
            uploadmodel returnValue = new uploadmodel();
            FileStream fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read);// 要上传的文件   
            BinaryReader r = new BinaryReader(fs);     
            string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");//时间戳   
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + strBoundary + "\r\n");     //请求头部信息   
            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(strBoundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append("file");
            sb.Append("\"; filename=\"");
            sb.Append(saveName);
            sb.Append("\";");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append("application/octet-stream");
            sb.Append("\r\n");
            sb.Append("\r\n");
            string strPostHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(strPostHeader);     
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(address));// 根据uri创建HttpWebRequest对象   
            httpReq.Method = "POST";    
            httpReq.AllowWriteStreamBuffering = false;      //对发送的数据不使用缓存   
            httpReq.Timeout = millisecond;           //设置获得响应的超时时间（300秒）   
            httpReq.ContentType = "multipart/form-data; boundary=" + strBoundary;
            long length = fs.Length + postHeaderBytes.Length + boundaryBytes.Length;
            long fileLength = fs.Length;
            httpReq.ContentLength = length;
            try
            {
                progressBar.Maximum = int.MaxValue;
                progressBar.Minimum = 0;
                progressBar.Value = 0;
                //每次上传4k  
                int bufferLength = 4096;
                byte[] buffer = new byte[bufferLength]; 
                long offset = 0;         //已上传的字节数   
                DateTime startTime = DateTime.Now;//开始上传时间   
                int size = r.Read(buffer, 0, bufferLength);
                Stream postStream = httpReq.GetRequestStream();         
                postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);//发送请求头部消息   
                while (size > 0)
                {
                    postStream.Write(buffer, 0, size);
                    offset += size;
                    progressBar.Value = (int)(offset * (int.MaxValue / length));
                    TimeSpan span = DateTime.Now - startTime;
                    double second = span.TotalSeconds;
                    lblTime.Text = "已用时：" + second.ToString("F2") + "秒";
                    if (second > 0.001)
                    {
                        lblSpeed.Text = "平均速度：" + (offset / 1024 / second).ToString("0.00") + "KB/秒";
                    }
                    else
                    {
                        lblSpeed.Text = " 正在连接…";
                    }
                    lblState.Text = "已上传：" + (offset * 100.0 / length).ToString("F2") + "%";
                    lblSize.Text = (offset / 1048576.0).ToString("F2") + "M/" + (fileLength / 1048576.0).ToString("F2") + "M";
                    Application.DoEvents();
                    size = r.Read(buffer, 0, bufferLength);
                }
                //添加尾部的时间戳   
                postStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                postStream.Close();        
                WebResponse webRespon = httpReq.GetResponse(); //获取服务器端的响应   
                Stream s = webRespon.GetResponseStream();
                //读取服务器端返回的消息  
                StreamReader sr = new StreamReader(s);
                String sReturnString = sr.ReadLine();
                s.Close();
                sr.Close();
                JObject obj = (JObject)JsonConvert.DeserializeObject(sReturnString);
                returnValue = new uploadmodel(obj);
            }
            catch
            {
                returnValue.msg = "上传失败！";
            }
            finally
            {
                fs.Close();
                r.Close();
            }
            return returnValue;
        }

        private class uploadmodel
        {
            public int code { get; set; }
            public string url { get; set; }
            public string msg { get; set; }
            public uploadmodel() { this.code = 1; }
            public uploadmodel(JObject obj)
            {
                this.code = Convert.ToInt32(obj.GetValue("error_code"));
                this.msg = obj.GetValue("error").ToString();
                if (obj.GetValue("url") != null)
                {
                    this.url = obj.GetValue("url").ToString();
                }
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileName = listView1.SelectedItems[0].Text;
            uploadmodel res = Delete_Request("http://" + ServerInfo.ServerIP + ":7070/api/materiel/del_image", fileName);
            if (res.code == 0 || res.code == 20001)
            {
                MessageModel m = ctrl.delImage(new ImageModel(fileName, mid));
                MessageBox.Show(m.Msg);
                if (m.Code == 0)
                {
                    loadData(false);
                }
            }
            else
            {
                MessageBox.Show(res.msg);
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Stream s = util.Info("ftp://" + ServerInfo.ServerIP + "/", listView1.SelectedItems[0].Text);
                if (s != null)
                {
                    pictureBox1.Image = Image.FromStream(s);
                }
            }
            
        }

        private uploadmodel Delete_Request(string address, string delName)
        {
            uploadmodel returnValue = new uploadmodel();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address + "?name="+delName);
                request.Timeout = millisecond;
                request.Method = "POST";
                request.ContentLength = 0;
                request.ServicePoint.Expect100Continue = false;
                WebResponse response = (WebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string str = reader.ReadToEnd();
                JObject obj = (JObject)JsonConvert.DeserializeObject(str);
                returnValue = new uploadmodel(obj);
            }
            catch (Exception ex)
            {
                returnValue.msg = ex.ToString();
            }
            return returnValue;
        }
    }
}
