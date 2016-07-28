using System;
using System.Xml;
using System.IO;

namespace goods
{
	public class xmlDoc
	{
		XmlDocument xmldoc = new XmlDocument();
		public xmlDoc() 
		{ 
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 加载XML文件
		/// </summary>
		/// <param name="path">XML文件路径</param>
		/// <returns></returns>
		public bool LoadXmlFile(string path) 
		{
			try
			{
				xmldoc.Load(path);
				return true;
			}
			catch 
			{
				return false;
			}
		}

		/// <summary>
		/// 加载XML字符串
		/// </summary>
		/// <param name="XMLstring">待加载的XML字符串联</param>
		/// <returns></returns>
		public bool LoadXmlString(string xmlString) 
		{
			try
			{
                xmldoc.LoadXml(xmlString);
				return true;
			}
			catch 
			{
				return false;
			}
		}

		/// <summary>
		/// 将XMLDOC保存为XML文件
		/// </summary>
		/// <param name="path">文件保存路径</param>
		/// <returns></returns>
		public bool SaveXmlFile(string path) 
		{
			try
			{
                xmldoc.Save(path);
				return true;
			}
			catch(Exception ) 
			{
				return false;
			}
		}

		/// <summary>
		/// 通过节点名来取得值
		/// </summary>
		/// <param name="nodeName">节点名</param>
		/// <returns>节点值</returns>
		public string readByNodeName(string nodeName) 
		{
			StringReader strReader = new StringReader(xmldoc.OuterXml);
			XmlTextReader reader = new XmlTextReader(strReader);
			string strOut = "";
			try
			{
				while (reader.Read())
				{
					if (reader.Name == nodeName)
					{
						strOut = reader.ReadString();
						break;
					}
				}
			}
			catch 
			{
				strOut = "";
			}
			reader.Close();
			return strOut;
		}

		/// <summary>
		/// 读属性
		/// </summary>
		/// <param name="nodeName">节点名</param>
		/// <param name="attribleName">属性名</param>
		/// <returns></returns>
		public string readAttrible(string nodeName, string attribleName) 
		{
			StringReader strReader = new StringReader(xmldoc.OuterXml);
			XmlTextReader reader = new XmlTextReader(strReader);
			string strOut = "";
			try 
			{
				while (reader.Read()) 
				{
					if (reader.Name == nodeName) 
					{
						strOut = reader.GetAttribute(attribleName);
					}
				}
			}
			catch 
			{
				strOut = "";
			}
			reader.Close();
			return strOut;
		}
	}
}
