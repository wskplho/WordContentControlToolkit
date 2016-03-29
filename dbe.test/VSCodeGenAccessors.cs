﻿// ------------------------------------------------------------------------------
//<autogenerated>
//        This code was generated by Microsoft Visual Studio Team System 2005.
//
//        Changes to this file may cause incorrect behavior and will be lost if
//        the code is regenerated.
//</autogenerated>
//------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dbe.test
{
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class BaseAccessor {
    
    protected Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject m_privateObject;
    
    protected BaseAccessor(object target, Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType type) {
        m_privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(target, type);
    }
    
    protected BaseAccessor(Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType type) : 
            this(null, type) {
    }
    
    internal virtual object Target {
        get {
            return m_privateObject.Target;
        }
    }
    
    public override string ToString() {
        return this.Target.ToString();
    }
    
    public override bool Equals(object obj) {
        if (typeof(BaseAccessor).IsInstanceOfType(obj)) {
            obj = ((BaseAccessor)(obj)).Target;
        }
        return this.Target.Equals(obj);
    }
    
    public override int GetHashCode() {
        return this.Target.GetHashCode();
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class dbe_XmlUtilAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType("dbe", "dbe.XmlUtil");
    
    internal dbe_XmlUtilAccessor(object target) : 
            base(target, m_privateType) {
    }
    
    internal static bool FSchema(string sXml) {
        object[] args = new object[] {
                sXml};
        bool ret = ((bool)(m_privateType.InvokeStatic("FSchema", new System.Type[] {
                    typeof(string)}, args)));
        return ret;
    }
    
    internal static string PrettyPrint(string sXml) {
        object[] args = new object[] {
                sXml};
        string ret = ((string)(m_privateType.InvokeStatic("PrettyPrint", new System.Type[] {
                    typeof(string)}, args)));
        return ret;
    }
    
    internal static string GetNsFromStr(string sXml) {
        object[] args = new object[] {
                sXml};
        string ret = ((string)(m_privateType.InvokeStatic("GetNsFromStr", new System.Type[] {
                    typeof(string)}, args)));
        return ret;
    }
    
    internal static object CreatePrivate() {
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject("dbe", "dbe.XmlUtil", new object[0]);
        return priv_obj.Target;
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class dbe_DbeCoreAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType("dbe", "dbe.DbeCore");
    
    internal dbe_DbeCoreAccessor(object target) : 
            base(target, m_privateType) {
    }
    
    internal global::dbe.Dal m_dal {
        get {
            global::dbe.Dal ret = ((global::dbe.Dal)(m_privateObject.GetField("m_dal")));
            return ret;
        }
        set {
            m_privateObject.SetField("m_dal", value);
        }
    }
    
    internal global::dbe.test.dbe_DbeCore_DataSourceAccessor m_ds {
        get {
            object _ret_val = m_privateObject.GetField("m_ds");
            global::dbe.test.dbe_DbeCore_DataSourceAccessor _ret = null;
            if ((_ret_val != null)) {
                _ret = new global::dbe.test.dbe_DbeCore_DataSourceAccessor(_ret_val);
            }
            global::dbe.test.dbe_DbeCore_DataSourceAccessor ret = _ret;
            return ret;
        }
        set {
            m_privateObject.SetField("m_ds", value);
        }
    }
    
    internal global::dbe.UiFmMain m_ui {
        get {
            global::dbe.UiFmMain ret = ((global::dbe.UiFmMain)(m_privateObject.GetField("m_ui")));
            return ret;
        }
        set {
            m_privateObject.SetField("m_ui", value);
        }
    }
    
    internal string m_sPkgFullName {
        get {
            string ret = ((string)(m_privateObject.GetField("m_sPkgFullName")));
            return ret;
        }
        set {
            m_privateObject.SetField("m_sPkgFullName", value);
        }
    }
    
    internal bool m_fLoaded {
        get {
            bool ret = ((bool)(m_privateObject.GetField("m_fLoaded")));
            return ret;
        }
        set {
            m_privateObject.SetField("m_fLoaded", value);
        }
    }
    
    internal bool m_fDirtied {
        get {
            bool ret = ((bool)(m_privateObject.GetField("m_fDirtied")));
            return ret;
        }
        set {
            m_privateObject.SetField("m_fDirtied", value);
        }
    }
    
    internal global::dbe.Dal Dal {
        get {
            global::dbe.Dal ret = ((global::dbe.Dal)(m_privateObject.GetProperty("Dal")));
            return ret;
        }
    }
    
    internal bool FLoaded {
        get {
            bool ret = ((bool)(m_privateObject.GetProperty("FLoaded")));
            return ret;
        }
        set {
            m_privateObject.SetProperty("FLoaded", value);
        }
    }
    
    internal bool FDirtied {
        get {
            bool ret = ((bool)(m_privateObject.GetProperty("FDirtied")));
            return ret;
        }
        set {
            m_privateObject.SetProperty("FDirtied", value);
        }
    }
    
    internal string PkgFullName {
        get {
            string ret = ((string)(m_privateObject.GetProperty("PkgFullName")));
            return ret;
        }
        set {
            m_privateObject.SetProperty("PkgFullName", value);
        }
    }
    
    internal string AppName {
        get {
            string ret = ((string)(m_privateObject.GetProperty("AppName")));
            return ret;
        }
    }
    
    internal string Version {
        get {
            string ret = ((string)(m_privateObject.GetProperty("Version")));
            return ret;
        }
    }
    
    internal global::dbe.UiFmMain Ui {
        get {
            global::dbe.UiFmMain ret = ((global::dbe.UiFmMain)(m_privateObject.GetProperty("Ui")));
            return ret;
        }
    }
    
    internal static object CreatePrivate(global::dbe.UiFmMain ui) {
        object[] args = new object[] {
                ui};
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject("dbe", "dbe.DbeCore", new System.Type[] {
                    typeof(global::dbe.UiFmMain)}, args);
        return priv_obj.Target;
    }
    
    internal void Load(string sPkgFullName, global::dbe.test.dbe_DbeCore_DataSourceAccessor ds) {
        object ds_val_target = null;
        if ((ds != null)) {
            ds_val_target = ds.Target;
        }
        object[] args = new object[] {
                sPkgFullName,
                ds_val_target};
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType target = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType("dbe", "dbe.DbeCore+DataSource");
        m_privateObject.Invoke("Load", new System.Type[] {
                    typeof(string),
                    target.ReferencedType}, args);
    }
    
    internal void Save() {
        object[] args = new object[0];
        m_privateObject.Invoke("Save", new System.Type[0], args);
    }
    
    internal void Close() {
        object[] args = new object[0];
        m_privateObject.Invoke("Close", new System.Type[0], args);
    }
    
    internal void Clear() {
        object[] args = new object[0];
        m_privateObject.Invoke("Clear", new System.Type[0], args);
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class dbe_DbeCore_DataSourceAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType("dbe", "dbe.DbeCore+DataSource");
    
    internal dbe_DbeCore_DataSourceAccessor(object target) : 
            base(target, m_privateType) {
    }
    
    internal static global::dbe.test.dbe_DbeCore_DataSourceAccessor OpenXmlPackage {
        get {
            object _ret_val = m_privateType.GetStaticField("OpenXmlPackage");
            global::dbe.test.dbe_DbeCore_DataSourceAccessor _ret = null;
            if ((_ret_val != null)) {
                _ret = new global::dbe.test.dbe_DbeCore_DataSourceAccessor(_ret_val);
            }
            global::dbe.test.dbe_DbeCore_DataSourceAccessor ret = _ret;
            return ret;
        }
    }
    
    internal static global::dbe.test.dbe_DbeCore_DataSourceAccessor WordOM {
        get {
            object _ret_val = m_privateType.GetStaticField("WordOM");
            global::dbe.test.dbe_DbeCore_DataSourceAccessor _ret = null;
            if ((_ret_val != null)) {
                _ret = new global::dbe.test.dbe_DbeCore_DataSourceAccessor(_ret_val);
            }
            global::dbe.test.dbe_DbeCore_DataSourceAccessor ret = _ret;
            return ret;
        }
    }
    
    internal static global::dbe.test.dbe_DbeCore_DataSourceAccessor Unknown {
        get {
            object _ret_val = m_privateType.GetStaticField("Unknown");
            global::dbe.test.dbe_DbeCore_DataSourceAccessor _ret = null;
            if ((_ret_val != null)) {
                _ret = new global::dbe.test.dbe_DbeCore_DataSourceAccessor(_ret_val);
            }
            global::dbe.test.dbe_DbeCore_DataSourceAccessor ret = _ret;
            return ret;
        }
    }
}
}