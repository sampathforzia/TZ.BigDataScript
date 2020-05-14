﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZ.CompExtention.Builder.Data;
namespace TZ.ImportDesk
{
    public partial class TalentozComponent : Form
    {
        public TalentozComponent()
        {
            InitializeComponent();
        }
        Int32 ClientID;
        string connection = "Server=183.82.34.174;Initial Catalog=talentozdev;Uid=admin;Pwd=admin312";
        DataTable dtComponentInstance = new DataTable();
        List<TZ.CompExtention.Builder.TalentozComponent> TZComponents = new List<CompExtention.Builder.TalentozComponent>();
        string ComponentName = "";
        private void TalentozComponent_Load(object sender, EventArgs e)
        {
            bindClients();
            GetComponentList();
        }

        private void bindFields() {
            lvComponentAttribute.Items.Clear();
           var c= TZComponents.Where(x => x.ComponentName == ComponentName).FirstOrDefault();
            if (c != null) {
                dtComponentInstance.DefaultView.RowFilter = "CompType = " + c.ComponentID;
             DataTable dtAtt=   dtComponentInstance.DefaultView.ToTable(true);
                dtComponentInstance.DefaultView.RowFilter = "";
                CompExtention.Builder.FieldElement f = new CompExtention.Builder.FieldElement();

                foreach (DataRow dr in dtAtt.Rows) {
                    f= Newtonsoft.Json.JsonConvert.DeserializeObject<CompExtention.Builder.FieldElement>(dr["FieldAttribute"].ToString());
                    f.FieldInstanceID = Convert.ToInt32(dr["FieldInstanceID"].ToString());
                    f.FieldDescription =  (dr["FieldDescription"].ToString());
                    var row = new string[] { f.FieldDescription,f.FieldTypeID.ToString(),f.isCore.ToString(), f.isReadOnly.ToString(),f.MaxLength.ToString() };
                    var li = new ListViewItem(row);
                    lvComponentAttribute.Items.Add(li);
                }
            }            
        }
        private void GetComponentList() {
            DataTable dtComp = new DataTable();
            dtComponentInstance = CompExtention.Shared.GetComponentList(ClientID, connection);
            dtComp = dtComponentInstance.DefaultView.ToTable(true, "CompType", "CompAttribute");
            lstComponentList.Items.Clear();
            foreach (DataRow s in dtComp.Rows)
            {
                var ft = s["CompType"];
                TZ.CompExtention.Builder.TalentozComponent comp =
                     Newtonsoft.Json.JsonConvert.DeserializeObject<TZ.CompExtention.Builder.TalentozComponent>(s["CompAttribute"].ToString());
                comp.ComponentID = Convert.ToInt32( ft);
                TZComponents.Add(comp);
                 var li = new ListViewItem(comp.ComponentName);
                lstComponentList.Items.Add(li);
            }
        }
        private void bindClients()
        {
            DataTable dtClient = new DataTable();
            dtClient = CompExtention.Shared.GetClientList(connection);
            cmbClients.ValueMember = "ClientID";
            cmbClients.DisplayMember = "CustomerName";
            cmbClients.DataSource = dtClient;
        }

        private void cmbClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView dr = (DataRowView)cmbClients.SelectedItem;
            ClientID = Convert.ToInt32(dr.Row["ClientID"]);
        }

        private void lstComponentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstComponentList.SelectedItems.Count > 0) {
                ComponentName = (string)lstComponentList.SelectedItems[0].Text;
                bindFields();
            }
        
        }

        private void btnImportComponent_Click(object sender, EventArgs e)
        {
            // TZ.CompExtention.Component
            //TZComponents
            DataTable dtLookUpComponent = new DataTable();
            List<CompExtention.Builder.FieldElement> TalentozComponentFields = new List<CompExtention.Builder.FieldElement>();
            List<int> LookUpComponent = new List<int>();
            List<CompExtention.Builder.FieldElement > LookupComponentDisplayField = new List<CompExtention.Builder.FieldElement >();  
            TZ.CompExtention.ComponentManager cm = new CompExtention.ComponentManager();
            cm.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));

            foreach (DataRow dr in dtComponentInstance.Rows)
            {
                CompExtention.Builder.FieldElement f = new CompExtention.Builder.FieldElement();
                f = Newtonsoft.Json.JsonConvert.DeserializeObject<CompExtention.Builder.FieldElement>(dr["FieldAttribute"].ToString());
                f.FieldInstanceID = Convert.ToInt32(dr["FieldInstanceID"].ToString());
                f.FieldDescription = (dr["FieldDescription"].ToString());
                f.ComponentID = Convert.ToInt32(dr["CompType"].ToString());
                f.FieldTypeID = Convert.ToInt32(dr["FieldTypeID"].ToString());
                if (f.FieldTypeID == 22) {
                    if (f.FieldComponent != "") {
                        LookUpComponent.Add(Convert.ToInt32(f.ComponentID));
                     var fComponent=   TZComponents.Where(x => x.ComponentID == Convert.ToInt32(f.FieldComponent)).FirstOrDefault();
                        f.FieldHelp = fComponent.TitleField;

                        LookupComponentDisplayField.Add( f);

                        //var a = LookupComponentDisplayField.Where(x => x.Key == Convert.ToInt32(f.FieldComponent)).ToList();
                        //if (a.Count > 0)
                        //{
                        //    a[0].Value.Add(f);
                        //}
                        //else {
                        //    var fc = new List<CompExtention.Builder.FieldElement>();
                        //    fc.Add(f);
                        //    LookupComponentDisplayField.Add(Convert.ToInt32(f.FieldComponent), fc);
                        //} 

                    }                   
                }
                TalentozComponentFields.Add(f);
            }
            LookUpComponent= LookUpComponent.Distinct().ToList();
            Dictionary<int, string> tzComponentWithImportComp = new Dictionary<int, string>();

        
            //foreach (int lc in LookUpComponent) {
            // var tzc=   TZComponents.Where(x => x.ComponentID == lc).FirstOrDefault();
            //    if (tzc != null) {
            //        TZ.CompExtention.Component Comp = new CompExtention.Component(tzc.ComponentName, GetType(tzc.ComponentType));
            //        Comp.TableName = tzc.TableName;
            //        var flist = TalentozComponentFields.Where(x => x.ComponentID == lc).ToList();

            //        CompExtention.Attribute _att_id1 = Comp.NewAttribute(ClientID);
            //        _att_id1.IsKey = true;
            //        _att_id1.Name = tzc.IDField1Name;
            //        _att_id1.DisplayName = tzc.IDField1Name;
            //        _att_id1.Type = CompExtention.AttributeType._number;
            //        _att_id1.IsNullable = false;
            //        Comp.AddAttribute(_att_id1);
            //        Comp.Keys.Add(_att_id1);

            //        CompExtention.Attribute _att_id2 = Comp.NewAttribute(ClientID);
            //        _att_id2.IsKey = true;
            //        _att_id2.Name = tzc.IDField2Name;
            //        _att_id2.DisplayName = tzc.IDField2Name;
            //        _att_id2.Type = CompExtention.AttributeType._number;
            //        _att_id2.IsNullable = false;
            //        Comp.AddAttribute(_att_id2);
            //        Comp.Keys.Add(_att_id2);

            //        if (tzc.IDField3Name != "") {
            //            CompExtention.Attribute _att_id3 = Comp.NewAttribute(ClientID);
            //            _att_id3.IsKey = true;
            //            _att_id3.Name = tzc.IDField3Name;
            //            _att_id3.DisplayName = tzc.IDField3Name;
            //            _att_id3.Type = CompExtention.AttributeType._number;
            //            _att_id3.IsNullable = false;
            //            Comp.AddAttribute(_att_id3);
            //            Comp.Keys.Add(_att_id3);
            //        }
            //        if (tzc.IDField4Name != "")
            //        {
            //            CompExtention.Attribute _att_id4 = Comp.NewAttribute(ClientID);
            //            _att_id4.IsKey = true;
            //            _att_id4.Name = tzc.IDField4Name;
            //            _att_id4.DisplayName = tzc.IDField4Name;
            //            _att_id4.Type = CompExtention.AttributeType._number;
            //            _att_id4.IsNullable = false;
            //            Comp.AddAttribute(_att_id4);
            //            Comp.Keys.Add(_att_id4);
            //        }

            //        foreach (CompExtention.Builder.FieldElement f in flist) {
            //    //        f = Newtonsoft.Json.JsonConvert.DeserializeObject<CompExtention.Builder.FieldElement>(dr["FieldAttribute"].ToString());
            //          //  f.FieldInstanceID = Convert.ToInt32(dr["FieldInstanceID"].ToString());
            //          //  f.FieldDescription = (dr["FieldDescription"].ToString());
            //            CompExtention.Attribute _att = Comp.NewAttribute(ClientID);
            //            _att.Name = "F_" + f.FieldInstanceID;
            //            _att.DisplayName = f.FieldDescription;
            //            _att.DefaultValue = f.DefaultValue;
            //            _att.FileExtension = f.FileExtension;
            //            _att.LookupInstanceID = f.FieldInstanceLookUpID.ToString();
            //            _att.Length = f.MaxLength;
            //            _att.IsUnique = f.isUnique;
            //            _att.IsRequired = f.isRequired;
            //            _att.IsNullable = true;
            //            _att.IsCore = f.isCore;
            //            _att.Type = GetAttributeType(f.FieldTypeID);
            //            if (f.FieldTypeID == 22)
            //            {
            //                if (f.FieldComponent != "")
            //                {
            //                    var fieldComp = TZComponents.Where(x => x.ComponentID == Convert.ToInt32(f.FieldComponent)).FirstOrDefault();
            //                    _att.ComponentLookupDisplayField = fieldComp.TitleField;
            //                    _att.ComponentLookup = f.FieldComponent.ToString();
            //                }
            //            }                      
            //            _att.IsAuto = false;
            //            _att.IsSecured = false;
            //            Comp.AddAttribute(_att);
            //        }
            //        if (cm.Save(Comp)) {
            //            tzComponentWithImportComp.Add(tzc.ComponentID,cm.Component.ID);
            //            foreach (KeyValuePair<int, List<CompExtention.Builder.FieldElement>> c in LookupComponentDisplayField) {
            //                if (c.Key   == tzc.ComponentID) {
            //                    foreach (CompExtention.Builder.FieldElement fc in c.Value) {
            //                        var cAtt = Comp.Attributes.Where(x => x.Name == fc.FieldHelp).FirstOrDefault();
            //                        fc.NewTextValue = cAtt.ID;
            //                    }                           
            //                   // 
            //                }
            //            }
            //        }
            //    }
            //}

                foreach (CompExtention.Builder.TalentozComponent comp in TZComponents) {               

                TZ.CompExtention.Component Comp = new CompExtention.Component(comp.ComponentName, Global. GetType( comp.ComponentType));                 
                if (comp != null)
                {
                    Comp.TableName = comp.TableName;                 
                    dtComponentInstance.DefaultView.RowFilter = "FieldTypeID = " + comp.ComponentID;
                    DataTable dtAtt = dtComponentInstance.DefaultView.ToTable(true);
                    dtComponentInstance.DefaultView.RowFilter = "";
                    //CompExtention.Builder.FieldElement f = new CompExtention.Builder.FieldElement();

                    CompExtention.Attribute _att_id1 = Comp.NewAttribute(ClientID);
                    _att_id1.IsKey = true;
                    _att_id1.Name = comp.IDField1Name;
                    _att_id1.DisplayName = comp.IDField1Name;
                    _att_id1.Type = CompExtention.AttributeType._number;
                    _att_id1.IsNullable = false;
                    Comp.AddAttribute(_att_id1);
                    Comp.Keys.Add(_att_id1);

                    CompExtention.Attribute _att_id2 = Comp.NewAttribute(ClientID);
                    _att_id2.IsKey = true;
                    _att_id2.Name = comp.IDField2Name;
                    _att_id2.DisplayName = comp.IDField2Name;
                    _att_id2.Type = CompExtention.AttributeType._number;
                    _att_id2.IsNullable = false;
                    Comp.AddAttribute(_att_id2);
                    Comp.Keys.Add(_att_id2);

                    if (comp.IDField3Name != "")
                    {
                        CompExtention.Attribute _att_id3 = Comp.NewAttribute(ClientID);
                        _att_id3.IsKey = true;
                        _att_id3.Name = comp.IDField3Name;
                        _att_id3.DisplayName = comp.IDField3Name;
                        _att_id3.Type = CompExtention.AttributeType._number;
                        _att_id3.IsNullable = false;
                        Comp.AddAttribute(_att_id3);
                        Comp.Keys.Add(_att_id3);
                    }
                    if (comp.IDField4Name != "")
                    {
                        CompExtention.Attribute _att_id4 = Comp.NewAttribute(ClientID);
                        _att_id4.IsKey = true;
                        _att_id4.Name = comp.IDField4Name;
                        _att_id4.DisplayName = comp.IDField4Name;
                        _att_id4.Type = CompExtention.AttributeType._number;
                        _att_id4.IsNullable = false;
                        Comp.AddAttribute(_att_id4);
                        Comp.Keys.Add(_att_id4);
                    }

                    var flist = TalentozComponentFields.Where(x => x.ComponentID == comp.ComponentID).ToList();

                    foreach (CompExtention.Builder.FieldElement f in flist)
                    {                       
                        CompExtention.Attribute _att = Comp.NewAttribute(ClientID);
                        _att.Name = "F_" + f.FieldInstanceID;
                        _att.DisplayName = f.FieldDescription;
                        _att.DefaultValue = f.DefaultValue;
                        _att.FileExtension = f.FileExtension;
                        _att.LookupInstanceID = f.FieldInstanceLookUpID.ToString();
                        _att.Length = f.MaxLength;
                        _att.IsUnique = f.isUnique;
                        _att.IsRequired = f.isRequired;
                        _att.IsNullable = true;
                        _att.Type = Global.GetAttributeType(f.FieldTypeID);
                        _att.IsCore = f.isCore;
                        if (f.FieldTypeID == 22) {
                            //if (f.FieldComponent != "") {
                            //var fieldComp= LookupComponentDisplayField.Where(x => x.Key == Convert.ToInt32(f.FieldComponent)).FirstOrDefault();
                            //    var cc = tzComponentWithImportComp.Where(x => x.Key == Convert.ToInt32(f.FieldComponent)).FirstOrDefault();
                            //    foreach (CompExtention.Builder.FieldElement fc in fieldComp.Value)
                            //    {
                            //       // var cAtt = Comp.Attributes.Where(x => x.Name == fc.FieldHelp).FirstOrDefault();
                            //       // fc.NewTextValue = cAtt.ID;
                            //        _att.ComponentLookupDisplayField = fc.NewTextValue;
                            //    }                               
                            //    //if (cc != null) {
                            //        _att.ComponentLookup = cc.Value;
                            //   // }                                
                            //}                      

                        }                   
                     
                        _att.IsAuto = false;
                        _att.IsSecured = false;
                        Comp.AddAttribute(_att);
                    }                   
                    cm.Save(Comp);
                    tzComponentWithImportComp.Add(comp.ComponentID, cm.Component.ID); // mapping talentoz component to import component ids.
                    var compLookup_imp = Comp.Attributes.Where(x => x.Type == CompExtention.AttributeType._componentlookup);
                    foreach ( CompExtention. Attribute a in compLookup_imp) {
                       // LookupComponentDisplayField.Add(Convert.ToInt32(f.FieldComponent), f);
                       var cdfield= LookupComponentDisplayField.Where(x => x.FieldDescription == a.DisplayName && x.ComponentID == comp.ComponentID ).FirstOrDefault();
                        cdfield.FieldValue = a.ID;
                     //   cdfield.Value.FileExtension = a.ComponentID;
                    }

                    foreach (CompExtention.Builder.FieldElement a in LookupComponentDisplayField) {
                       var atc= Comp.Attributes.Where(x => x.Name == a.FieldHelp).FirstOrDefault();
                        if (atc != null) {
                            a.FieldExpression = atc.ID;
                        }
                    }
                }
            }
            foreach ( CompExtention.Builder.FieldElement  a in LookupComponentDisplayField)
            {
               var k= tzComponentWithImportComp.Where(x => x.Key == Convert.ToInt32(a.FieldComponent)).FirstOrDefault();
                
                cm.UpdateComponentLookup(ClientID, "", a.FieldValue, k.Value, a.FieldExpression);
            }
                MessageBox.Show("done");
        }
      
    }
}