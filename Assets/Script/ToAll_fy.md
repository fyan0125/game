# 加入場景物件(AI無法走過)
1. Inspector > ☑ Static  
2. Window > AI > Navigation > Object > Walkable  
3. Navigation > Bake > Bake  
<br/>
## 會移動的物件(AI無法走過)
1. Add Component > Nav Mesh Obstacle > ☑ Carve  
2. Window > AI > Navigation > Object > Walkable  
<br/>
# 地形
1. Inspector > Layer > Ground
* 最外圍要包一層牆壁，射擊才不會出問題
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
* 刪除道具叉叉 : RemoveButton(如果做把東西丟到垃圾桶,要改程式碼)  
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>

兔：有什麼事情嗎？
主：這裡是⋯⋯？
兔：這裏？這裡是復甦之地呀，看來你是新來的。
主：復甦之地⋯⋯？
兔：對呀！讓我先教你幾項功能吧

通過上下左右來控制方向
空白鍵是跳躍
Tab鍵是切換技能

主：原來如此
兔：看你還這麼弱，我就送你一項技能好了
—幸運兔腳，當技能切換至幸運兔腳時跳躍將會變成超級跳
（到這裡時才能使用超級跳躍）
兔：我能幫你的就到這了，剩下的就交給你自己去探索啦！




>增加背包空間  
>>Inventory > space 

>可刪
>>Material
