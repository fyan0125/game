# 加入場景物件(人物無法走過)
1. Inspector > ☑ Static  
2. Window > AI > Navigation > Object > Walkable  
3. Navigation > Bake > Bake  
<br/>
## 會移動的物件(人物無法走過)
1. Add Component > Nav Mesh Obstacle > ☑ Carve  
2. Window > AI > Navigation > Object > Walkable  
<br/>
# 地形
1. Inspector > Layer > Ground
<br/>
# 可互動物件
1. Script > Interactable  
2. Inspector > Script > Interaction Transform > 指定自己
<br/>
## 指定方向才能互動
1. Create Empty (children) > 移到指定方向
2. Inspector > Script > Interaction Transform > 指定該children  
<br/>
# 道具
1. Project > Create > Inventory > Item  
2. Inspector(Item) > Name & Icon
3. Hierarchy > create prop 
4. Inspector(prop) > Script(ItemPickedUp) > 指定item   
<br/>
# 背包UI
>script done
* 背包底圖 : ItemParent
* 背包格底圖 : ItemButton
* 刪除道具叉叉 : RemoveButton(如果做把東西丟到垃圾桶,要改程式碼)  
* 控制背包開關 : InventoryUI.cs (可改)
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>





>增加背包空間  
>>Inventory > space 

>可刪
>>Material
