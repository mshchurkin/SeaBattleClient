   M S B A M L   `   `   `  ���� J  FSeaBattleClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:SeaBattleClientSeaBattleClient  '    !SeaBattleClient.BattleFieldWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:SeaBattleClient       � Title$  BattleFieldWindow��$	��436��$	��807��.��� 
����  �.+��� -   $��	mainLabel��$	��Top=�$��Center=�$��32(�
��� E��$��IMPACT)�.��%#Расставьте корабли� .+�&   System.Windows.Controls.DataGrid  $	��300��$	��300��$
��Left=�$��
20,80,0,10q�
����� �   AutoGenerateColumns .     	CellStyle ��  �� 
TargetType*  $System.Windows.Controls.DataGridCell"  .�-      Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$  ��  $ A��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 28   $ B��$ 28   $ C��$ 28   $ D��$ 28   $ E��$ 28   $ F��$ 28   $ G��$ 28   $ H��$ 28   $ I��$ 28   $ J��$ 28 .@�    SeaBattleClient.BattleCell    $	��300��$	��300��$��Right=�$��
0,80,20,10q�
����� � .     $  ��  $ A��$ 28   $ B��$ 28   $ C��$ 28   $ D��$ 28   $ E��$ 28   $ F��$ 28   $ G��$ 28   $ H��$ 28   $ I��$ 28   $ J��$ 28 � .+�	� -   $��VertRad��$��Center=�$��Center=�$��По вертикали��$��0,0,12,0q�
���  �� d� 	IsChecked$
 TrueY�	� -   $��HorRad��$��Center=�$��Center=�$!��По горизонтали��$��0,40,0,0q�
���  ��$ FalseY�� .+�	� -   $��	fourCheck��$��Center=�$��Center=�$$��Четырехпалубный��$��	0,150,0,0q�
���  ��$
 TrueY�	� -   $��
threeCheck��$��Center=�$��Center=�$��Трехпалубный��$��
0,190,20,0q�
���  ��$ FalseY�	� -   $��twoCheck��$��Center=�$��Center=�$��Двухпалубный��$��
0,230,20,0q�
���  ��$ FalseY�	� -   $��oneCheck��$��Center=�$��Center=�$��Однопалубный��$��
0,270,15,0q�
���  ��$ FalseY�