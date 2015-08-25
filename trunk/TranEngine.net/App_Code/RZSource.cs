using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using TrainEngine.Core;

/// <summary>
///RZSource 的摘要说明
/// </summary>
public class RZSource
{
	private RZSource()
	{
		
	}
    private string root = Utils.ApplicationRoot() + "RZSource";
    private static RZSource _init;
    /// <summary>
    /// Create RZSource Object
    /// </summary>
    public static RZSource Init
    {
        get {
            if (_init == null)
            {
                _init = new RZSource();
            }
            return _init;
        }
    }
    private List<string> _rzTypeList;
    public List<string> RzTypeList
    {
        get
        {
            if (_rzTypeList==null)
            {
                _rzTypeList = GetRzTypeList(string.Empty);

            }
            return _rzTypeList;
        }
    }

    private List<RzViewContent> _Rz_产品认证;
    public List<RzViewContent> Rz_产品认证
    {
        get
        {
            if (_Rz_产品认证 == null)
            {
                _Rz_产品认证 = GetRzSourceByType("产品认证", "");
            }
            return _Rz_产品认证;
        }
    }

    private List<RzViewContent> _Rz_体系认证;
    public List<RzViewContent> Rz_体系认证
    {
        get
        {
            if (_Rz_体系认证 == null)
            {
                _Rz_体系认证 = GetRzSourceByType("体系认证", "");
            }
            return _Rz_体系认证;
        }
    }

    private List<RzViewContent> _Sf_体系认证;
    public List<RzViewContent> Sf_体系认证
    {
        get
        {
            if (_Sf_体系认证 == null)
            {
                _Sf_体系认证 = GetRzSourceByType("体系认证收费", "认证收费");
            }
            return _Sf_体系认证;
        }
    }

    private List<RzViewContent> _Sf_产品认证;
    public List<RzViewContent> Sf_产品认证
    {
        get
        {
            if (_Sf_产品认证 == null)
            {
                _Sf_产品认证 = GetRzSourceByType("产品认证收费", "认证收费");
            }
            return _Sf_产品认证;
        }
    }

    private List<string> GetRzTypeList(string parent)
    {
        string[] types = Directory.GetDirectories(root+@"\"+parent);

        List<string> list = new List<string>();
        for (int i = 0; i < types.Length; i++)
        {
            list.Add(types[i]);
        }
        return list;
    }

    public List<RzViewContent> GetRzSourceByType(string rzType, string parent)
    {
        List<RzViewContent> list = new List<RzViewContent>();
        string[] rzText = Directory.GetFiles(root + @"\" + parent + @"\" + rzType);
        for (int i = 0; i < rzText.Length; i++)
        {
            string filePath = rzText[i];
            using (StreamReader sr = File.OpenText(filePath))
            {
                string content = sr.ReadToEnd();
                sr.Close();

                RzViewContent rvc = new RzViewContent(Path.GetFileName(filePath).Replace(".txt", ""), content ,rzType);
                list.Add(rvc);
            }
        }
        return list;
    }
}