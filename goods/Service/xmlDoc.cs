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
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ����XML�ļ�
		/// </summary>
		/// <param name="path">XML�ļ�·��</param>
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
		/// ����XML�ַ���
		/// </summary>
		/// <param name="XMLstring">�����ص�XML�ַ�����</param>
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
		/// ��XMLDOC����ΪXML�ļ�
		/// </summary>
		/// <param name="path">�ļ�����·��</param>
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
		/// ͨ���ڵ�����ȡ��ֵ
		/// </summary>
		/// <param name="nodeName">�ڵ���</param>
		/// <returns>�ڵ�ֵ</returns>
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
		/// ������
		/// </summary>
		/// <param name="nodeName">�ڵ���</param>
		/// <param name="attribleName">������</param>
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
