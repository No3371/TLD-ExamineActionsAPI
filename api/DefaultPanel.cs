#define VERY_VERBOSE
using Il2Cpp;
using Il2CppAK.Wwise;
using Il2CppTLD.Gear;
using Il2CppTLD.IntBackedUnit;
using MelonLoader;
using UnityEngine;

namespace ExamineActionsAPI
{
    public class DefaultPanel : IExamineActionPanel
    {
        
		internal GameObject repairPanelClone/*, toolSelectionClone*/, materialsClone, yieldsClone, chanceClone, durationClone, buttonClone, buttonRoot;
		internal List<HarvestRepairMaterial> Materials, Products;
		internal List<UILabel> materialChances, productChances;
		internal InfoBlock info1, info2, infoChance, infoDuration;
		internal UISprite bg1, bg2;
		Vector2 info1Pos, info2Pos;
		internal UIButton buttonContinue, buttonSubLeft, buttonSubRight;
		internal UILabel buttonLabel, materiaslLabel, bottomWarningLabel, consumeLabel, stopLabel;
		public Panel_Inventory_Examine PIE { get; }
		public Panel_Repair PR { get; set; }
		public bool IsExtension => false;
        bool infoBlockExtended;

        public DefaultPanel(Panel_Inventory_Examine pie, Panel_Repair pr)
        {
			repairPanelClone = GameObject.Instantiate(pie.m_RepairPanel, pie.m_RepairPanel.transform.parent);
            Transform gameobjects = FindChildWrapper(repairPanelClone.transform, "GameObject");
			// toolSelectionClone = GameObject.Instantiate(pie.m_ActionToolSelect, pie.m_ActionToolSelect.transform.parent);
			chanceClone = FindChildWrapper(repairPanelClone.transform, "GameObject/ChanceLabels").gameObject;
            Transform origAmountLabel = FindChildWrapper(repairPanelClone.transform, "GameObject/AmountLabels");
			durationClone = FindChildWrapper(repairPanelClone.transform, "GameObject/TimeLabels").gameObject;
            Transform origSkillLabel = FindChildWrapper(repairPanelClone.transform, "GameObject/SkillLabels");

			info1 = new InfoBlock(GameObject.Instantiate(origAmountLabel.gameObject, gameobjects));
			info1.go.transform.localPosition = info1Pos = durationClone.transform.localPosition + new Vector3(0, 24);
			info2 = new InfoBlock(GameObject.Instantiate(origAmountLabel.gameObject, gameobjects));
			info2.go.transform.localPosition = info2Pos = chanceClone.transform.localPosition + new Vector3(0, 24);
			info1.go.SetActive(false);
			info2.go.SetActive(false);

			bg1 = gameobjects.transform.FindChild("BG (1)").GetComponent<UISprite>();
			bg2 = gameobjects.transform.FindChild("BG (2)").GetComponent<UISprite>();
			bg2.transform.localPosition = new (bg2.transform.localPosition.x, bg2.transform.localPosition.y - 70, bg2.transform.localPosition.z);
			bg2.height += 80;
			SetInfoBlock1Row();
			SetInfoBlock2Rows();

			infoChance = new InfoBlock(chanceClone, chanceClone.transform.FindChild("Header").GetComponent<UILabel>(), chanceClone.transform.FindChild("Repair_ChanceLabel_Value").GetComponent<UILabel>());
			infoDuration = new InfoBlock(durationClone, durationClone.transform.FindChild("Header").GetComponent<UILabel>(), durationClone.transform.FindChild("Repair_TimeLabel_Value").GetComponent<UILabel>());

			infoDuration.label1.text = "Time Needed";

            chanceClone.transform.localPosition = new(chanceClone.transform.localPosition.x, 124, chanceClone.transform.localPosition.z);
            durationClone.transform.localPosition = new(durationClone.transform.localPosition.x, 124, durationClone.transform.localPosition.z);
					// MelonLogger.Msg($"1");

            // skillClone = GameObject.Instantiate(__instance.m_ReadPanel.transform.FindChild("GameObject/Skill").gameObject, gameobjects);
            // skillClone.transform.localPosition = new Vector3(skillClone.transform.localPosition.x, 115, skillClone.transform.localPosition.z);
            // skillClone.transform.FindChild("ProgressBar").gameObject.SetActive(false);

            materialsClone = FindChildWrapper(repairPanelClone.transform, "GameObject/RequiredMaterials").gameObject;
            FindChildWrapper(materialsClone.transform, "Label_RequiredMaterials").localPosition = new Vector3(0, 72, 0);
			Materials = new (5);
            foreach (var m in materialsClone.GetComponentsInChildren<HarvestRepairMaterial>(true))
            {
                if (m.gameObject.name == "Item1")
                    Materials.Add(m);
                else
                    GameObject.Destroy(m.gameObject);
            }
			Materials.Add(GameObject.Instantiate(Materials[0], Materials[0].transform.parent));
			Materials.Add(GameObject.Instantiate(Materials[0], Materials[0].transform.parent));
			Materials.Add(GameObject.Instantiate(Materials[0], Materials[0].transform.parent));
			Materials.Add(GameObject.Instantiate(Materials[0], Materials[0].transform.parent));

            for (int i = 0; i < Materials.Count; i++)
            {
                Materials[i].gameObject.name = $"Item{i+1}";
            }
					// MelonLogger.Msg($"2: {__instance.m_HarvestYieldRoot}");

			yieldsClone = GameObject.Instantiate(materialsClone, gameobjects);
            yieldsClone.name = "Yield";
            FindChildWrapper(yieldsClone.transform, "Label_RequiredMaterials").localPosition = new Vector3(0, 72, 0);
            FindChildWrapper(yieldsClone.transform, "Label_RequiredMaterials").GetComponent<UILocalize>().key = "GAMEPLAY_Yield";
			Products = new (5);
            // ! This is required for yields too because Destroy is not immediate
            foreach (var m in yieldsClone.GetComponentsInChildren<HarvestRepairMaterial>(true))
            {
                if (m.gameObject.name == "Item1")
                    Products.Add(m);
                else
                    GameObject.Destroy(m.gameObject);
            }
			Products.Add(GameObject.Instantiate(Products[0], Products[0].transform.parent));
			Products.Add(GameObject.Instantiate(Products[0], Products[0].transform.parent));
			Products.Add(GameObject.Instantiate(Products[0], Products[0].transform.parent));
			Products.Add(GameObject.Instantiate(Products[0], Products[0].transform.parent));
            for (int i = 0; i < Products.Count; i++)
            {
                Products[i].gameObject.name = $"Item{i+1}";
            }

			materialChances = new (Materials.Count);
			for (int i = 0; i < Materials.Count; i++)
			{
				materialChances.Add(GameObject.Instantiate(Materials[0].m_StackLabel, Materials[i].transform));
				materialChances[i].transform.localPosition = new Vector2(40, -26);
				materialChances[i].color = new Color(0.9044f, 0.6985f, 0.3059f);
				Materials[i].m_StackLabel.transform.localPosition = new Vector2(40, -40);
			}

			productChances = new (Products.Count);
			for (int i = 0; i < Products.Count; i++)
			{
				productChances.Add(GameObject.Instantiate(Products[0].m_StackLabel, Products[i].transform));
				productChances[i].transform.localPosition = new Vector2(40, -26);
				productChances[i].color = new Color(0.9044f, 0.6985f, 0.3059f);
				Products[i].m_StackLabel.transform.localPosition = new Vector2(40, -40);
                Products[i].m_StackLabel.color = Color.white;
			}

            
			for (int i = 0; i < Materials.Count; i++)
                Materials[i].transform.localPosition = Vector3.zero;
			for (int i = 0; i < Products.Count; i++)
                Products[i].transform.localPosition = Vector3.zero;

            // pie.m_RepairToolsList.m_ToolscrollList.CreateFromList
            GameObject.Destroy(origAmountLabel.gameObject);
            GameObject.Destroy(origSkillLabel.gameObject);
            // MelonLogger.Msg($"3");

            buttonRoot = FindChildWrapper(repairPanelClone.transform, "RepairPanel_Buttons").gameObject;
            buttonRoot.transform.localPosition = new (buttonRoot.transform.localPosition.x, buttonRoot.transform.localPosition.y - 50, buttonRoot.transform.localPosition.z);
            buttonRoot.GetComponentInChildren<UIControllerScheme>().enabled = false;
			buttonRoot.gameObject.SetActive(true);
            buttonClone = FindChildWrapper(repairPanelClone.transform, "RepairPanel_Buttons/Mouse/ButtonRepair").gameObject;
			foreach (var b in buttonClone.GetComponents<UIButton>())
			{
				if (b.onClick.Count == 0) continue;
				b.onClick.Clear();
				EventDelegate.Add(b.onClick, new System.Action(ExamineActionsAPI.Instance.OnPerformSelectedAction));

				buttonContinue = b;
				buttonLabel = b.GetComponentInChildren<UILabel>();
				MonoBehaviour.Destroy(buttonLabel.GetComponent<UILocalize>());
			}

			bottomWarningLabel = FindChildWrapper(repairPanelClone.transform, "RequiresToolLabel").GetComponent<UILabel>();
			materiaslLabel = FindChildWrapper(materialsClone.transform, "Label_RequiredMaterials").GetComponent<UILabel>();

            buttonSubLeft = GameObject.Instantiate(pie.m_RepairToolsList.m_Button_ToolDecrease, buttonClone.transform.parent).GetComponent<UIButton>();
            buttonSubRight = GameObject.Instantiate(pie.m_RepairToolsList.m_Button_ToolIncrease, buttonClone.transform.parent).GetComponent<UIButton>();
            buttonSubLeft.transform.localPosition = buttonClone.transform.localPosition + new Vector3(-160, 0);
            buttonSubRight.transform.localPosition = buttonClone.transform.localPosition + new Vector3(160, 0);
            buttonSubLeft.onClick[0] = new EventDelegate(new System.Action(ExamineActionsAPI.Instance.OnPreviousSubAction));
            buttonSubRight.onClick[0] = new EventDelegate(new System.Action(ExamineActionsAPI.Instance.OnNextSubAction));

            consumeLabel = GameObject.Instantiate(bottomWarningLabel, buttonClone.transform.parent).GetComponent<UILabel>();
            MonoBehaviour.Destroy(consumeLabel.GetComponent<UIAnchor>());
            consumeLabel.transform.localPosition = buttonClone.transform.localPosition + new Vector3(0, -24);
            consumeLabel.color += new Color(0f, 0.3f, -0.1f);

            stopLabel = GameObject.Instantiate(bottomWarningLabel, buttonClone.transform.parent).GetComponent<UILabel>();
            MonoBehaviour.Destroy(stopLabel.GetComponent<UIAnchor>());
            stopLabel.transform.localPosition = buttonClone.transform.localPosition + new Vector3(0, -44);
            stopLabel.color += new Color(0f, 0.3f, -0.1f);

			Toggle(false);
            PIE = pie;
			ExamineActionsAPI.VeryVerboseLog($"Set PIE {PIE.name} {PIE.GetInstanceID()}");
			PR = pr;
			ExamineActionsAPI.VeryVerboseLog($"Set PR {PR.name} {PR.GetInstanceID()}");
		}

        Transform FindChildWrapper (Transform transform, string childPath)
        {
            var target = transform.FindChild(childPath);
            if (target == null)
            {
                ExamineActionsAPI.Instance.LoggerInstance.Error($"Failed to grab official UI gameobject: {childPath} from {transform.name} (this is likely caused by UI changed by game update or other mods)");
                var lastSep = childPath.LastIndexOf('/');
                if (lastSep != -1) ExamineActionsAPI.Instance.LoggerInstance.Error($"Traversing up...");
                while (lastSep != -1)
                {
                    childPath = childPath.Substring(0, lastSep);
                    target = transform.FindChild(childPath);
                    if (target == null)
                        ExamineActionsAPI.Instance.LoggerInstance.Error($"Failed to find {childPath}");
                    else
                    {
                        ExamineActionsAPI.Instance.LoggerInstance.Error($"Found {childPath}");
                        break;
                    }

                    lastSep = childPath.LastIndexOf('/');
                }
                target = null;
            }
            return target;
        }

        void IExamineActionPanel.OnActionDeselected(ExamineActionState state) {}

        void IExamineActionPanel.OnActionSelected(ExamineActionState state)
        {
            materialsClone.SetActive(false);
            yieldsClone.SetActive(false);
            PIE.GetActionToolSelect().SetActive(false);
            bg2.gameObject.SetActive(false);
            bottomWarningLabel.gameObject.SetActive(false);
            int subs = state.Action.GetSubActionCount(state);
            ExamineActionsAPI.VeryVerboseLog($"Sub {state.SubActionId + 1} / {subs}");
            buttonSubLeft.gameObject.SetActive(state.SubActionId > 0);
            buttonSubRight.gameObject.SetActive(state.SubActionId < subs - 1);
            bool extendInfoBlock = false;
            UpdateConsumeLabel(state);
            UpdateStopLabel(state);
            MaybeSetupMaterials(state);
            MaybeSetupProducts(state);
            MaybeSetupTools(state);
            UpdateInfoBlock(state);

            
            bool displayingMats = state.GetAllMaterialCount() > 0;
            bool displayingProducts = state.GetAllProductCount() > 0;
            int upperY = infoBlockExtended ? -60 : -24;
            int lowerY = infoBlockExtended ? -200 : -184;
            if (displayingMats && displayingProducts)
            {
                materialsClone.transform.localPosition = new Vector3(materialsClone.transform.localPosition.x, upperY, materialsClone.transform.localPosition.z);
                yieldsClone.transform.localPosition = new Vector3(yieldsClone.transform.localPosition.x, lowerY, yieldsClone.transform.localPosition.z);
            }
            else if (displayingMats)
            {
                materialsClone.transform.localPosition = new Vector3(materialsClone.transform.localPosition.x, infoBlockExtended ? -120 : -96, materialsClone.transform.localPosition.z);
            }
            else if (displayingProducts)
            {
                yieldsClone.transform.localPosition = new Vector3(yieldsClone.transform.localPosition.x, infoBlockExtended ? -120 : -96, yieldsClone.transform.localPosition.z);
            }



            buttonLabel.text = state.Action.ActionButtonLocalizedString.Text();
        }

        private void UpdateInfoBlock(ExamineActionState state)
        {
            infoDuration.label2.text = Utils.GetExpandedDurationString(state.ActiveActionDurationMinutes.Value);

            bool showingChance = false;
            if (state.Action is IExamineActionFailable eaf)
            {
                infoChance.label2.text = string.Format("{0:0.0}%", state.ActiveSuccessChance);
                infoChance.go.SetActive(true);
                showingChance = true;
            }
            else
            {
                infoChance.go.SetActive(false);
            }

            info1.label1.color = ExamineActionsAPI.NORMAL_COLOR;
            info1.label2.color = ExamineActionsAPI.NORMAL_COLOR;
            info2.label1.color = ExamineActionsAPI.NORMAL_COLOR;
            info2.label2.color = ExamineActionsAPI.NORMAL_COLOR;
            if (!ExamineActionsAPI.DISABLE_CUSTOM_INFO
             && state.Action is IExamineActionCustomInfo eaci)
            {
                var conf1 = eaci.GetInfo1(state);
                var conf2 = eaci.GetInfo2(state);
                if ((showingChance && conf1 != null) || (showingChance && conf2 != null) || (conf1 != null && conf2 != null))
                    SetInfoBlock2Rows();
                else SetInfoBlock1Row();

                if (showingChance)
                {
                    info1.go.transform.localPosition = info1Pos;
                    info2.go.transform.localPosition = info2Pos;
                }
                else
                {
                    info1.go.transform.localPosition = infoChance.go.transform.localPosition;
                    info2.go.transform.localPosition = info1Pos;
                }

                SetupCustomInfos(eaci, state, conf1, conf2);
            }
            else
            {
                info1.go.SetActive(false);
                info2.go.SetActive(false);
                SetInfoBlock1Row();
            }
        }

        private void MaybeSetupMaterials(ExamineActionState state)
        {
            List<MaterialOrProductItemConf> materials = null;
            if (state.Action is IExamineActionRequireItems erm)
            {
                materials = new (1);
                erm.GetRequiredItems(state, materials);
            }
            List<MaterialOrProductLiquidConf> liquids = null;
            if (state.Action is IExamineActionRequireLiquid erl)
            {
                liquids = new(1);
                erl.GetRequireLiquid(state, liquids);
            }
            List<MaterialOrProductPowderConf> powders = null;
            if (state.Action is IExamineActionRequirePowder erp)
            {
                powders = new(1);
                erp.GetRequiredPowder(state, powders);
            }

            int matCount = materials?.Count ?? 0;
            int liqCount = liquids?.Count ?? 0;
            int powCount = powders?.Count ?? 0;
            int total = matCount + liqCount + powCount;
            ExamineActionsAPI.VeryVerboseLog($"Materials: {matCount} items / {liqCount} liquid / {powCount} powders");
            if (total == 0) return;

            for (int configured = 0; configured < this.Materials.Count; configured++)
            {
                var slot = this.Materials[configured];
                if (configured >= total)
                {
                    slot.Hide();
                    continue;
                }
                ExamineActionsAPI.VeryVerboseLog($"Configuring material#{configured+1}/{total}");
    
                Vector3 localPosition = slot.transform.localPosition;
				localPosition.x = 0;
				localPosition.x += PR.m_RequiredMaterialSpacing * ((float)configured - (total / 2f - 0.5f)) / (total >=4? 2f: 1.2f);
				slot.transform.localPosition = localPosition;
				slot.transform.localScale = total >= 4 ? new Vector3(0.7f, 0.7f, 1) : new Vector3(1f, 1f, 1);


				if (configured >= matCount + liqCount) // Powder
                {
					ExamineActionsAPI.VeryVerboseLog($"- Powder");
					int pIdx = configured - (matCount + liqCount);
                    var conf = powders[pIdx];
                    ExamineActionsAPI.VeryVerboseLog($"Showing {conf.Type.name} {conf.Kgs}kg");
                    
                    if (conf.Chance < 100)
                    {
                        this.materialChances[configured].text = $"{conf.Chance}%";
                        this.materialChances[configured].gameObject.SetActive(true);
                    }
                    else this.materialChances[configured].gameObject.SetActive(false);

                    slot.ShowPowder(conf.Type, ItemWeight.FromKilograms(conf.Kgs));

                    ItemWeight owned = GameManager.m_Inventory.GetTotalPowderWeight(conf.Type);
                    PowderItem? subjectPowderItem = state.Subject?.m_PowderItem;
                    if (subjectPowderItem != null && subjectPowderItem.m_Type == conf.Type) owned -= subjectPowderItem.m_Weight;
                    ExamineActionsAPI.VeryVerboseLog($"Found {conf.Type.name} {owned}kg (+{state.Subject?.m_PowderItem?.m_Weight.ToQuantity(1f) ?? 0})");
                    float required = conf.Kgs;
                    for (int j = 0; j < pIdx; j++)
                        if (conf.Type == powders[j].Type) required += powders[j].Kgs;
                    ExamineActionsAPI.VeryVerboseLog($"Requires {conf.Type.name} {required}/{owned}kg");
                    slot.m_GearLabel.color = owned.ToQuantity(1f) >= required ? new Color(1, 1, 1) : slot.m_RedColorToUse;
                }
                else if (configured >= matCount) // Liquid
                {
					ExamineActionsAPI.VeryVerboseLog($"- Liquid");
					int idx = configured - matCount;
                    var conf = liquids[idx];
                    ExamineActionsAPI.VeryVerboseLog($"Showing {conf.Type.name} {conf.Liters}l");
                    
                    if (conf.Chance < 100)
                    {
                        this.materialChances[configured].text = $"{conf.Chance}%";
                        this.materialChances[configured].gameObject.SetActive(true);
                    }
                    else this.materialChances[configured].gameObject.SetActive(false);

                    GearItem? containerPrefab = conf.Type?.DefaultContainer?.PrefabReference?.GetOrLoadTypedAsset();
                    if (containerPrefab == null)
                        containerPrefab = conf.Type?.DefaultContainer?.PrefabReference?.GetOrLoadTypedAsset();
                    if (containerPrefab == null)
                    {
                        if (conf.Type.DefaultContainer == null)
                            MelonLogger.Error($"No default container set for liquid {conf.Type.name}.");
                        else if (conf.Type?.DefaultContainer?.PrefabReference == null)
                            MelonLogger.Error($"No prefab preference of default container set for liquid {conf.Type.name}.");
                        MelonLogger.Error($"Failed to load prefab for container of liquid {conf.Type.name}, contact the author of EAAPI.");
                    }
                    slot.ShowItem(containerPrefab, 1, true);
                    slot.m_StackLabel.text = $"{conf.Liters}L";
                    slot.m_StackLabel.gameObject.SetActive(true);

                    ExamineActionsAPI.VeryVerboseLog($"Showing {conf.Type.name} {conf.Liters}L");
                    ItemLiquidVolume owned = GameManager.m_Inventory.GetTotalLiquidVolume(conf.Type);
                    LiquidItem? subjectLiquidItem = state.Subject?.m_LiquidItem;
                    if (subjectLiquidItem != null
                     && subjectLiquidItem.m_LiquidType == conf.Type)
                        owned -= subjectLiquidItem.m_Liquid;
                    ExamineActionsAPI.VeryVerboseLog($"Found {conf.Type.name} {owned}l (+{subjectLiquidItem?.m_Liquid.ToQuantity(ItemLiquidVolume.Liter.m_Units) ?? 0})");
                    float required = conf.Liters;
                    for (int j = 0; j < idx; j++)
                        if (conf.Type == liquids[j].Type) required += liquids[j].Liters;
                    ExamineActionsAPI.VeryVerboseLog($"Requires {conf.Type.name} {required}/{owned}L");
                    slot.m_GearLabel.color = owned.ToQuantity(ItemLiquidVolume.Liter.m_Units) >= required ? new Color(1, 1, 1) : slot.m_RedColorToUse;
                }
                else
                {
					ExamineActionsAPI.VeryVerboseLog($"- Other ?");
					var conf = materials[configured];
                    int stackNum = conf.Units;
                    int checkingStackNum = conf.Units;
                    if (state.Subject.name == conf.GearName) checkingStackNum += state.Subject.m_StackableItem?.m_Units?? 1;
                    for (int j = 0; j < configured; j++)
                        if (conf.GearName == materials[j].GearName) checkingStackNum += materials[j].Units;
                    var invItem = GameManager.GetInventoryComponent().GearInInventory(conf.GearName, checkingStackNum);
                    GearItem prefab = GearItem.LoadGearItemPrefab(conf.GearName);
                    if (prefab == null)
                        MelonLogger.Error($"Invalid material: {conf.GearName}. There will be exception following this line of log. Check do you have the item mod installed or contact the mod providing this action or this action recipe.");

                    if (conf.Chance < 100)
                    {
                        this.materialChances[configured].text = $"{conf.Chance}%";
                        this.materialChances[configured].gameObject.SetActive(true);
                    }
                    else this.materialChances[configured].gameObject.SetActive(false);
                    slot.ShowItem(prefab, conf.Units, invItem != null);
                }
            }
            if (state.AllMaterialsReady ?? false) materiaslLabel.color = PIE.m_RepairMaterialsTable.m_RequiredMaterialsLabelDefaultColor;
            else materiaslLabel.color = PIE.m_RepairLabelColorDisabled;
            materialsClone.SetActive(true);
            if (!bg2.gameObject.activeSelf) bg2.gameObject.SetActive(true);
        }

        private void MaybeSetupProducts(ExamineActionState state)
        {
            List<MaterialOrProductItemConf> items;
            List<MaterialOrProductLiquidConf> liquids;
            List<MaterialOrProductPowderConf> powders;
            state.GetAllProducts(out items, out liquids, out powders);

            int matCount = items?.Count ?? 0;
            int liqCount = liquids?.Count ?? 0;
            int powCount = powders?.Count ?? 0;
            int total = matCount + liqCount + powCount;
            if (total == 0) return;

            for (int configured = 0; configured < this.Products.Count; configured++)
            {
                var slot = this.Products[configured];
                if (configured >= total)
                {
                    slot.Hide();
                    continue;
                }

                Vector3 localPosition = slot.transform.localPosition;
                localPosition.x = 0;
                localPosition.x += PR.m_RequiredMaterialSpacing * ((float)configured - (total / 2f - 0.5f)) / (total >= 4 ? 2f : 1.2f);
                slot.transform.localPosition = localPosition;
                slot.transform.localScale = total >= 4 ? new Vector3(0.7f, 0.7f, 1) : new Vector3(1f, 1f, 1);

                if (configured >= matCount + liqCount) // Powder
                {
                    var conf = powders[configured - (matCount + liqCount)];

                    if (conf.Chance < 100)
                    {
                        this.productChances[configured].text = $"{conf.Chance}%";
                        this.productChances[configured].gameObject.SetActive(true);
                    }
                    else this.productChances[configured].gameObject.SetActive(false);

                    slot.ShowPowder(conf.Type, ItemWeight.FromKilograms(conf.Kgs));
                }
                else if (configured >= matCount) // Liquid
                {
                    var conf = liquids[configured - matCount];

                    if (conf.Chance < 100)
                    {
                        this.productChances[configured].text = $"{conf.Chance}%";
                        this.productChances[configured].gameObject.SetActive(true);
                    }
                    else this.productChances[configured].gameObject.SetActive(false);


                    GearItem? containerPrefab = conf.Type?.DefaultContainer?.PrefabReference?.GetOrLoadTypedAsset();
                    if (containerPrefab == null)
                        containerPrefab = conf.Type?.DefaultContainer?.PrefabReference?.GetOrLoadTypedAsset();
                    slot.ShowItem(containerPrefab, 1, true);
                    slot.m_StackLabel.text = $"{conf.Liters} L";
                    slot.m_StackLabel.gameObject.SetActive(true);
                    ExamineActionsAPI.VeryVerboseLog($"Showing {conf.Type.name} {conf.Liters}L");
                }
                else
                {
                    var conf = items[configured];
                    GearItem prefab = GearItem.LoadGearItemPrefab(conf.GearName);
                    if (prefab == null)
                        MelonLogger.Error($"Invalid prodcut: {conf.GearName}. There will be exception following this line of log. Contact mod providing this action or this action recipe.");

                    if (conf.Chance < 100)
                    {
                        this.productChances[configured].text = $"{conf.Chance}%";
                        this.productChances[configured].gameObject.SetActive(true);
                    }
                    else this.productChances[configured].gameObject.SetActive(false);
                    slot.ShowItem(prefab, conf.Units);
                }
            }
            yieldsClone.SetActive(true);
            if (!bg2.gameObject.activeSelf) bg2.gameObject.SetActive(true);
        }

        void IExamineActionPanel.OnPerformingAction(ExamineActionState state) {}

        void IExamineActionPanel.OnSelectingTool(ExamineActionState state)
        {
            bg2.gameObject.SetActive(true);
            yieldsClone.gameObject.SetActive(false);
            materialsClone.gameObject.SetActive(false);
        }

        void IExamineActionPanel.OnToolSelected(ExamineActionState state)
        {
            bg2.gameObject.SetActive(false);
        }
		public void Toggle (bool toggle)
		{
			repairPanelClone.SetActive(toggle);
		}
		public void SetupCustomInfos (IExamineActionCustomInfo act, ExamineActionState state, InfoItemConfig? conf1, InfoItemConfig? conf2)
		{
			if (conf1 != null && conf2 != null)
			{
				this.info1.label1.text = conf1.Title.Text();
                this.info1.label1.color = conf1.LabelColor;
				this.info1.label2.text = conf1.Content;
                this.info1.label2.color = conf1.ContentColor;
				this.info2.label1.text = conf2.Title.Text();
                this.info2.label1.color = conf2.LabelColor;
				this.info2.label2.text = conf2.Content;
                this.info2.label2.color = conf2.ContentColor;
				this.info1.go.SetActive(true);
				this.info2.go.SetActive(true);
			}
			else if (conf1 != null && conf2 == null)
			{
				this.info1.label1.text = conf1.Title.Text();
                this.info1.label1.color = conf1.LabelColor;
				this.info1.label2.text = conf1.Content;
                this.info1.label2.color = conf1.ContentColor;
				this.info1.go.SetActive(true);
				this.info2.go.SetActive(false);
			}
			else if (conf1 == null && conf2 != null)
			{
				this.info1.label1.text = conf2.Title.Text();
                this.info2.label1.color = conf2.LabelColor;
				this.info1.label2.text = conf2.Content;
                this.info2.label2.color = conf2.ContentColor;
				this.info1.go.SetActive(true);
				this.info2.go.SetActive(false);
			}
		}

        private void MaybeSetupTools(ExamineActionState state)
        {
            if (state.Action is IExamineActionRequireTool eat)
            {
                if (PIE.m_RepairToolsList.m_Tools.Count == 0)
                {
                    bottomWarningLabel.gameObject.SetActive(true);
                    bottomWarningLabel.text = Localization.Get("GAMEPLAY_ToolRequired");
                }

				PIE.m_ToolScrollList.CleanUp();
				PIE.m_ToolScrollList.CreateList(PIE.m_RepairToolsList.m_Tools.Count);
                for (int k = 0; k < PIE.m_RepairToolsList.m_Tools.Count; k++)
                {
                    UITexture componentInChildren = Utils.GetComponentInChildren<UITexture>(PIE.m_ToolScrollList.m_ScrollObjects[k]);
                    if (PIE.m_RepairToolsList.m_Tools[k] != null)
                    {
                        GearItem component7 = PIE.m_RepairToolsList.m_Tools[k].GetComponent<GearItem>();
                        if (component7)
                        {
                            componentInChildren.mainTexture = Utils.GetInventoryIconTexture(component7);
                        }
                    }
                    else
                    {
                        componentInChildren.mainTexture = Utils.GetInventoryGridIconTexture(PIE.m_RepairToolsList.m_NoToolSpriteName);
                        componentInChildren.color = PIE.m_RepairToolsList.m_NoToolSpriteColor;
                        componentInChildren.transform.localScale = PIE.m_RepairToolsList.m_NoToolSpriteSizeModifier;
                    }
                }
                PIE.m_RepairToolsList.RefreshSelectedActionTool();
            }
            else bottomWarningLabel.gameObject.SetActive(false);
        }

        void UpdateStopLabel (ExamineActionState state)
        {
            var cancellable = state.Action is IExamineActionCancellable;
            var interruptable = state.Action is IExamineActionInterruptable;
            stopLabel.gameObject.SetActive(true);

            if (cancellable && interruptable)
                stopLabel.text = "Can be cancelled (ESC) / May be interrupted";
            else if (!cancellable && interruptable)
                stopLabel.text = "Can not cancelled / May be interrupted";
            else if (cancellable && !interruptable)
                stopLabel.text = "Can be cancelled (ESC)";
            else if (!cancellable && !interruptable)
                stopLabel.text = "Must be finished";

        }

        void UpdateConsumeLabel (ExamineActionState state)
        {
            var consumeS = state.Action.ConsumeOnSuccess(state);
            var consumeF = (state.Action as IExamineActionFailable)?.ConsumeOnFailure(state) ?? false; // null means anyway not possible to happens
            var consumeC = (state.Action as IExamineActionCancellable)?.ConsumeOnCancel(state) ?? false;
            var consumeI = (state.Action as IExamineActionInterruptable)?.ConsumeOnInterruption(state) ?? false;
            
            if (!consumeS && !consumeF && !consumeC && !consumeI)
                consumeLabel.text = "Never consume";
            if (consumeS && consumeF && consumeC && consumeI)
                consumeLabel.text = "Always consume";
            else if (consumeS && consumeF && consumeC)
                consumeLabel.text = "Consume on: Success / Failure / Cancellation";
            else if (consumeF && consumeC && consumeI)
                consumeLabel.text = "Consume on: Failure / Cancellation / Interruption";
            else if (consumeS && consumeC && consumeI)
                consumeLabel.text = "Consume on: Success / Cancellation / Interruption";
            else if (consumeS && consumeF && consumeI)
                consumeLabel.text = "Consume on: Success / Failure / Interruption";
            else if (consumeS && consumeF)
                consumeLabel.text = "Consume on: Success / Failure";
            else if (consumeS && consumeC)
                consumeLabel.text = "Consume on: Success / Cancellation";
            else if (consumeS && consumeI)
                consumeLabel.text = "Consume on: Success / Interruption";
            else if (consumeF && consumeC)
                consumeLabel.text = "Consume on: Failure / Cancellation";
            else if (consumeF && consumeI)
                consumeLabel.text = "Consume on: Failure / Interruption";
            else if (consumeC && consumeI)
                consumeLabel.text = "Consume on: Cancellation / Interruption";
            else if (consumeS)
                consumeLabel.text = "Consume on: Success";
            else if (consumeF)
                consumeLabel.text = "Consume on: Failure";
            else if (consumeC)
                consumeLabel.text = "Consume on: Cancellation";
            else if (consumeI)
                consumeLabel.text = "Consume on: Interruption";

            int consuming = state.Action.OverrideConsumingUnits(state);
            if (consuming > 1)
                consumeLabel.text += $" (x{consuming})";
        }

        void SetInfoBlock1Row ()
		{
			bg1.transform.localPosition = new Vector3(bg1.transform.localPosition.x, 70, bg1.transform.localPosition.z);
			bg1.height = 76;
			bg2.height = 366;
			infoBlockExtended = false;
		}

		void SetInfoBlock2Rows ()
		{
			bg1.transform.localPosition = new Vector3(bg1.transform.localPosition.x, 22, bg1.transform.localPosition.z);
			bg2.transform.localPosition = new Vector3(bg2.transform.localPosition.x, -296, bg2.transform.localPosition.z);
			bg1.height = 124;
			bg2.height = 318;
			infoBlockExtended = true;
		}

        public void OnActionSucceed(ExamineActionState state) {}

        public void OnActionFailed(ExamineActionState state) {}

        public void OnActionCancelled(ExamineActionState state) {}

        public void OnActionInterrupted(ExamineActionState state, bool force) {}

        public void OnSelectingToolChanged(ExamineActionState state)
        {
            UpdateInfoBlock(state);
        }

        void IExamineActionPanel.OnBlockedPerformingAction(ExamineActionState state, PerformingBlockedReased reason)
        {
            bottomWarningLabel.gameObject.SetActive(true);
            bottomWarningLabel.text = reason switch
            {
                PerformingBlockedReased.Interruption => Localization.Get("Action will be interrupted (Sick/Hurt/Cold/Hungry...)") ?? null,
                PerformingBlockedReased.Action => Localization.Get("Action can not be performed") ?? null,
                PerformingBlockedReased.Requirements => Localization.Get("Action requirements not satisfied") ?? null,
            };
        }

        public class InfoBlock
		{
			internal GameObject go;
			internal UISprite bg;
			internal UILabel label1, label2;

            public InfoBlock(GameObject go)
            {
                this.go = go;
				bg = go.GetComponentInChildren<UISprite>();

                Transform header = go.transform.FindChild("Header");
                MonoBehaviour.Destroy(header.GetComponent<UILocalize>());

				label1 = header.GetComponent<UILabel>();
				label2 = go.transform.FindChild("Repair_AmountLabel_Value")?.gameObject?.GetComponent<UILabel>();

				label1.text = "-";
				label2.text = "---";
            }

            public InfoBlock(GameObject go, UILabel header, UILabel content)
            {
                this.go = go;
				bg = go.GetComponentInChildren<UISprite>();

                MonoBehaviour.Destroy(header.GetComponent<UILocalize>());
				label1 = header;
				label2 = content;
            }

			public void Set (string header, string content)
			{
				label1.text = header;
				label2.text = content;
			}
        }
    }
}
