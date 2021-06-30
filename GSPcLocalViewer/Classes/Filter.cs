// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.Classes.Filter
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System;
using System.Collections;
using System.Xml;

namespace GSPcLocalViewer.Classes
{
  internal class Filter
  {
    private frmViewer frmMain;

    public Filter(frmViewer fm)
    {
      this.frmMain = fm;
    }

    public XmlNodeList FilterBooksList(XmlNode xSchemaNode, XmlNodeList xNodeList)
    {
      ArrayList arrMandatory = new ArrayList();
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
      {
        if (attribute.Value.ToUpper().Equals("ID"))
          arrMandatory.Add((object) attribute.Name);
        else if (attribute.Value.ToUpper().Equals("PUBLISHINGID"))
          arrMandatory.Add((object) attribute.Name);
        else if (attribute.Value.ToUpper().Equals("UPDATEDATE"))
          arrMandatory.Add((object) attribute.Name);
      }
      if (this.frmMain.p_ArgsF != null && this.frmMain.p_ArgsF.Length > 0)
        return this.FilterNodeList(xNodeList, arrMandatory);
      return xNodeList;
    }

    public XmlNode FilterPage(XmlNode xSchemaNode, XmlNode xNode)
    {
      ArrayList arrMandatory = new ArrayList();
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
      {
        if (attribute.Value.ToUpper().Equals("ID"))
          arrMandatory.Add((object) attribute.Name);
        else if (attribute.Value.ToUpper().Equals("PAGENAME"))
          arrMandatory.Add((object) attribute.Name);
      }
      if (this.frmMain.p_ArgsF != null && this.frmMain.p_ArgsF.Length > 0)
        return this.FilterNode(xNode, arrMandatory);
      return xNode;
    }

    public XmlNodeList FilterPicsList(XmlNode xSchemaNode, XmlNodeList xNodeList)
    {
      ArrayList arrMandatory = new ArrayList();
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
      {
        if (attribute.Value.ToUpper().Equals("PICTUREFILE"))
          arrMandatory.Add((object) attribute.Name);
        else if (attribute.Value.ToUpper().Equals("UPDATEDATE"))
          arrMandatory.Add((object) attribute.Name);
        else if (attribute.Value.ToUpper().Equals("UPDATEDATEPIC"))
          arrMandatory.Add((object) attribute.Name);
      }
      if (this.frmMain.p_ArgsF != null && this.frmMain.p_ArgsF.Length > 0)
        return this.FilterNodeList(xNodeList, arrMandatory);
      return xNodeList;
    }

    public XmlNodeList FilterPartsList(XmlNode xSchemaNode, XmlNodeList xNodeList)
    {
      ArrayList arrMandatory = new ArrayList();
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
      {
        if (attribute.Value.ToUpper().Equals("ID"))
          arrMandatory.Add((object) attribute.Name);
        else if (attribute.Value.ToUpper().Equals("LINKNUMBER"))
          arrMandatory.Add((object) attribute.Name);
        else if (attribute.Value.ToUpper().Equals("PARTNUMBER"))
          arrMandatory.Add((object) attribute.Name);
      }
      if (this.frmMain.p_ArgsF != null && this.frmMain.p_ArgsF.Length > 0)
        return this.FilterNodeList(xNodeList, arrMandatory);
      return xNodeList;
    }

    private XmlNodeList FilterNodeList(XmlNodeList xNodeList, ArrayList arrMandatory)
    {
      foreach (XmlNode xNode in xNodeList)
      {
        if (xNode.HasChildNodes)
        {
          foreach (XmlNode childNode in xNode.ChildNodes)
          {
            if (childNode.Name.ToUpper() == "SEL")
            {
              if (childNode.Attributes.Count > 0 && childNode.Attributes[0].Name.ToUpper() == "TG")
              {
                string[] strArray1 = childNode.Attributes[0].Value.Split(new string[1]
                {
                  ","
                }, StringSplitOptions.RemoveEmptyEntries);
                bool flag = false;
                foreach (string str in strArray1)
                {
                  if (arrMandatory.Contains((object) str))
                  {
                    flag = true;
                    break;
                  }
                }
                foreach (string str in this.frmMain.p_ArgsF)
                {
                  string[] separator = new string[1]{ "=" };
                  int num = 1;
                  string[] strArray2 = str.Split(separator, (StringSplitOptions) num);
                  if (strArray2.Length == 2)
                  {
                    foreach (XmlAttribute attribute in (XmlNamedNodeMap) childNode.Attributes)
                    {
                      if (attribute.Name.ToUpper() == strArray2[0].ToUpper() && !this.ValueMatchesSelectFilter(strArray2[1], attribute.Value, false))
                      {
                        if (flag)
                        {
                          xNode.RemoveAll();
                          break;
                        }
                        foreach (string index in strArray1)
                        {
                          if (xNode.Attributes[index] != null)
                            xNode.Attributes.Remove(xNode.Attributes[index]);
                        }
                      }
                    }
                  }
                }
              }
            }
            else if (childNode.Name.ToUpper() == "SWT")
            {
              if (childNode.Attributes.Count > 0 && childNode.Attributes[0].Name.ToUpper() == "TG")
              {
                string[] strArray1 = childNode.Attributes[0].Value.Split(new string[1]
                {
                  ","
                }, StringSplitOptions.RemoveEmptyEntries);
                bool flag = false;
                foreach (string str in strArray1)
                {
                  if (arrMandatory.Contains((object) str))
                  {
                    flag = true;
                    break;
                  }
                }
                foreach (string str in this.frmMain.p_ArgsF)
                {
                  string[] separator = new string[1]{ "=" };
                  int num = 1;
                  string[] strArray2 = str.Split(separator, (StringSplitOptions) num);
                  if (strArray2.Length == 2)
                  {
                    foreach (XmlAttribute attribute in (XmlNamedNodeMap) childNode.Attributes)
                    {
                      if (attribute.Name.ToUpper() == strArray2[0].ToUpper() && strArray2[1].ToUpper() == "ON" && attribute.Value.ToUpper() == "ON")
                      {
                        if (flag)
                        {
                          xNode.RemoveAll();
                          break;
                        }
                        foreach (string index in strArray1)
                        {
                          if (xNode.Attributes[index] != null)
                            xNode.Attributes.Remove(xNode.Attributes[index]);
                        }
                      }
                    }
                  }
                }
              }
            }
            else if (childNode.Name.ToUpper() == "RNG" && childNode.Attributes.Count > 0 && childNode.Attributes[0].Name.ToUpper() == "TG")
            {
              string[] strArray1 = childNode.Attributes[0].Value.Split(new string[1]
              {
                ","
              }, StringSplitOptions.RemoveEmptyEntries);
              bool flag = false;
              foreach (string str in strArray1)
              {
                if (arrMandatory.Contains((object) str))
                {
                  flag = true;
                  break;
                }
              }
              foreach (string str in this.frmMain.p_ArgsF)
              {
                string[] separator = new string[1]{ "=" };
                int num = 1;
                string[] strArray2 = str.Split(separator, (StringSplitOptions) num);
                if (strArray2.Length == 2)
                {
                  foreach (XmlAttribute attribute in (XmlNamedNodeMap) childNode.Attributes)
                  {
                    if (attribute.Name.ToUpper() == strArray2[0].ToUpper() && !this.ValueInRangeFilter(attribute.Value, strArray2[1]))
                    {
                      if (flag)
                      {
                        xNode.RemoveAll();
                        break;
                      }
                      foreach (string index in strArray1)
                      {
                        if (xNode.Attributes[index] != null)
                          xNode.Attributes.Remove(xNode.Attributes[index]);
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      return xNodeList;
    }

    private XmlNode FilterNode(XmlNode xNode, ArrayList arrMandatory)
    {
      if (xNode.HasChildNodes)
      {
        foreach (XmlNode childNode in xNode.ChildNodes)
        {
          if (childNode.Name.ToUpper() == "SEL")
          {
            if (childNode.Attributes.Count > 0 && childNode.Attributes[0].Name.ToUpper() == "TG")
            {
              string[] strArray1 = childNode.Attributes[0].Value.Split(new string[1]
              {
                ","
              }, StringSplitOptions.RemoveEmptyEntries);
              bool flag = false;
              foreach (string str in strArray1)
              {
                if (arrMandatory.Contains((object) str))
                {
                  flag = true;
                  break;
                }
              }
              foreach (string str in this.frmMain.p_ArgsF)
              {
                string[] separator = new string[1]{ "=" };
                int num = 1;
                string[] strArray2 = str.Split(separator, (StringSplitOptions) num);
                if (strArray2.Length == 2)
                {
                  foreach (XmlAttribute attribute in (XmlNamedNodeMap) childNode.Attributes)
                  {
                    if (attribute.Name.ToUpper() == strArray2[0].ToUpper() && !this.ValueMatchesSelectFilter(strArray2[1], attribute.Value, false))
                    {
                      if (flag)
                      {
                        xNode.RemoveAll();
                        break;
                      }
                      foreach (string index in strArray1)
                      {
                        if (xNode.Attributes[index] != null)
                          xNode.Attributes.Remove(xNode.Attributes[index]);
                      }
                    }
                  }
                }
              }
            }
          }
          else if (childNode.Name.ToUpper() == "SWT")
          {
            if (childNode.Attributes.Count > 0 && childNode.Attributes[0].Name.ToUpper() == "TG")
            {
              string[] strArray1 = childNode.Attributes[0].Value.Split(new string[1]
              {
                ","
              }, StringSplitOptions.RemoveEmptyEntries);
              bool flag = false;
              foreach (string str in strArray1)
              {
                if (arrMandatory.Contains((object) str))
                {
                  flag = true;
                  break;
                }
              }
              foreach (string str in this.frmMain.p_ArgsF)
              {
                string[] separator = new string[1]{ "=" };
                int num = 1;
                string[] strArray2 = str.Split(separator, (StringSplitOptions) num);
                if (strArray2.Length == 2)
                {
                  foreach (XmlAttribute attribute in (XmlNamedNodeMap) childNode.Attributes)
                  {
                    if (attribute.Name.ToUpper() == strArray2[0].ToUpper() && strArray2[1].ToUpper() == "ON" && attribute.Value.ToUpper() == "ON")
                    {
                      if (flag)
                      {
                        xNode.RemoveAll();
                        break;
                      }
                      foreach (string index in strArray1)
                      {
                        if (xNode.Attributes[index] != null)
                          xNode.Attributes.Remove(xNode.Attributes[index]);
                      }
                    }
                  }
                }
              }
            }
          }
          else if (childNode.Name.ToUpper() == "RNG" && childNode.Attributes.Count > 0 && childNode.Attributes[0].Name.ToUpper() == "TG")
          {
            string[] strArray1 = childNode.Attributes[0].Value.Split(new string[1]
            {
              ","
            }, StringSplitOptions.RemoveEmptyEntries);
            bool flag = false;
            foreach (string str in strArray1)
            {
              if (arrMandatory.Contains((object) str))
              {
                flag = true;
                break;
              }
            }
            foreach (string str in this.frmMain.p_ArgsF)
            {
              string[] separator = new string[1]{ "=" };
              int num = 1;
              string[] strArray2 = str.Split(separator, (StringSplitOptions) num);
              if (strArray2.Length == 2)
              {
                foreach (XmlAttribute attribute in (XmlNamedNodeMap) childNode.Attributes)
                {
                  if (attribute.Name.ToUpper() == strArray2[0].ToUpper() && !this.ValueInRangeFilter(attribute.Value, strArray2[1]))
                  {
                    if (flag)
                    {
                      xNode.RemoveAll();
                      break;
                    }
                    foreach (string index in strArray1)
                    {
                      if (xNode.Attributes[index] != null)
                        xNode.Attributes.Remove(xNode.Attributes[index]);
                    }
                  }
                }
              }
            }
          }
        }
      }
      return xNode;
    }

    private bool ValueMatchesSelectFilter(string values1, string values2, bool caseSensitive)
    {
      string[] strArray1 = values1.Split(new string[1]
      {
        ","
      }, StringSplitOptions.RemoveEmptyEntries);
      string[] strArray2 = values2.Split(new string[1]
      {
        ","
      }, StringSplitOptions.RemoveEmptyEntries);
      foreach (string str1 in strArray1)
      {
        foreach (string str2 in strArray2)
        {
          if (caseSensitive)
          {
            if (str1.Equals(str2))
              return true;
          }
          else if (str1.ToUpper().Equals(str2.ToUpper()))
            return true;
        }
      }
      return false;
    }

    private bool ValueInRangeFilter(string range, string value)
    {
      string[] strArray = range.Split(new string[1]{ "**" }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length != 2)
        return false;
      int result1;
      if (int.TryParse(strArray[0], out result1))
      {
        int result2;
        if (int.TryParse(strArray[1], out result2))
        {
          if (result1 > result2)
          {
            int num = result1;
            result1 = result2;
            result2 = num;
          }
          int result3;
          return int.TryParse(value, out result3) && result3 >= result1 && result3 <= result2;
        }
      }
      try
      {
        DateTime dateTime1 = DateTime.ParseExact(strArray[0], "dd/MM/yyyy", (IFormatProvider) null);
        DateTime dateTime2 = DateTime.ParseExact(strArray[1], "dd/MM/yyyy", (IFormatProvider) null);
        DateTime exact = DateTime.ParseExact(value, "dd/MM/yyyy", (IFormatProvider) null);
        if (dateTime1 > dateTime2)
        {
          DateTime dateTime3 = dateTime1;
          dateTime1 = dateTime2;
          dateTime2 = dateTime3;
        }
        return exact >= dateTime1 && exact <= dateTime2;
      }
      catch
      {
        return false;
      }
    }
  }
}
