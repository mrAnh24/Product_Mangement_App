   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   M+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5   	       DatabaseApp.Update   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   DuserControls+clr-namespace:DatabaseApp.View.UserControls     5   	     � 
ResizeMode$  NoResize=�5   1    � WindowStartupLocation$ CenterScreen=�5       � Title$ Products Update��5	   	   $	��500��6!   $	��500��6.   .��5
      � 
�����F�$��
10,0,10,10q�6"    � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $
��0.3* �6   �� 5      �� 5      $
��0.2* �6   �� 5      .+�5   
   � $��0��6   
�������.+�5      +   %DatabaseApp.View.UserControls.MenuBar  $�� 0,0,0,5q�6#   5      � 5   
   $��1��6   .+�5      �� $��Update Products data��6   
�������$��	10,0,0,10q�6G   $��Center=�5      $��Center=�65   $��26(�6P   $��1��5      $��DemiBold"�6(   �� 5      -   5        $��
btnRefresh��5      $
��🔄��6'   $��Right=�64   $��24(�6P   $
��auto��6^   $��	0,0,10,10q�5       � Cursor$
 Hand��6B   $
��auto��6P   
������ $��Center=�5   .   5      � 5    
   $��2��6    � ColumnDefinitions 5!      �� 5"      $
��0.5* �6#   �� 5#      �� 5$      $
��0.6* �6#   .+�5'      � $��0��6   $��10,0,0,0q�6#   .+�5(      �� $��Center=�6   .+�5)      �� $��Product Code:��6    $��16(�65   $��Bottom=�5*       $��0,0,0,10q�6;   �� 5+      $��Product:��6    $��16(�60   $��Center=�5,       $��0,0,0,10q�6;   �� 5-      $��Description:��6    $��16(�64   $��Center=�5.       $��0,0,0,10q�6;   �� 5/      $��Price:��6    $��16(�6.   $	��Top=�50       $��0,0,0,10q�68   �� 51      $��Amount:��6    $��16(�6/   $	��Top=�52       $��0,0,0,10q�68   53      54      � 56      $��1��6   .+�57      �� $��Center=�6   .+�58      �� -   5        $��tbProductId��58      $��25��61   $��0,0,10,8q�6=   $��16(�59      {�.  6,   �� 5:      -   5        $��	tbProduct��5:      $��25��6/   $��0,0,10,8q�6;   $��16(�6M   5;      �� 5<      -   5        $��tbDescription��5<      $��25��63   $��0,0,10,8q�6?   $��16(�6Q   {�.  6_   �� 5=      -   5        $��tbPrice��5=      $��25��6-   $��0,0,10,8q�69   $��16(�6K   {�.  5>   J   �� 5?      -   5        $��tbAmount��5?      $��25��6.   $��0,0,10,8q�6:   $��16(�6L   {�.  5@   L   5A      �� 5C      -   5        $��cbType��5C      $��	0,58,10,0q�6)   $��Right=�6<   $
��auto��6X   
���   � �� 
IsEditable .   �� Text$
 Type��5D   A   
��� ���{�.  .@�5F      �� $
��Type��6#   {�.  62   �� 5G      $
��Meat��6#   �� 5H      $��Dairy��6#   �� 5I      $��	Vegetable��6#   �� 5J      $��Drink��6#   �� 5K      $��Fruit��6#   �� 5L      $��Dessert��6#   �� 5M      $��Snack��6#   �� 5N      $��Other��6#   5O      �� 5Q      -   5        $��cbStatus��5Q      $��
0,124,10,0q�6+   $��Right=�6?   $
��auto��6[   
���   � .  $ Status��5R   A   {�.  .@�5S      �� $��Status��6#   {�.  
��� ���6F   �� 5T      $��	Available��6#   �� 5U      $��Unavailable��6#   �� 5V      $��Sold Out��6#   �� 5W      $��Discontinue��6#   �� 5X      $��On sale��6#   5Y      5Z      � 5\      $��2��6   $��0,0,10,0q�6$   .+�5]      �� $	��120��6   $��D://cable.png��6%   $	��Top=�6<   �� 5^      $��Upload by: ��6   $��Center=�6/   $��	2,100,0,0q�6J   �� 5_      -	   5        $��txtUploader��5_      $	��...��6/   $��Center=�6:   $��	2,130,0,0q�5`      $��WrapWithOverflow=�6/   5a      5b      � 5d   
   $��3��6   $��	10,0,10,0q�6   
�����F� 5e      �� 5f      �� 5g      �� 5h      �� 5i      .+�5l      � $��0��6   .+�5m      �� -
   5        $��btnAdd��5m      $	��Add��6'   $��Center=�65   $
 Hand��5n   .   $��0,0,0,5q�6<   5o      � 5q      $��1��6   .+�5r      �� -   5        $��	btnUpdate��5r      $��Update��6*   $��Center=�6;   $
 Hand��5s   1   $��0,0,0,5q�6?   5t      � 5v      $��2��6   .+�5w      �� -   5        $��	btnDelete��5w      $��Delete��6*   $��Center=�6;   $
 Hand��5x   1   $��0,0,0,5q�6?   {�.  
���   �5y   +   5z      � 5|      $��3��6   .+�5}      �� -   5        $��btnClear��5}      $��Clear��6)   $��Center=�69   $
 Hand��5~   0   $��0,0,0,5q�6>   5      5�      � 5�   
   $��4��6   .+�5�      &   System.Windows.Controls.DataGrid  -   5        $��	dgProduct��5�      $��
10,0,10,10q�6(      
IsReadOnly . 	   SelectionMode$	 Single=�6N   $��3��5�      
   AlternatingRowBackground

 ����� �� AlternationCount$ 2��6P      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6e      CanUserResizeRows .     AutoGenerateColumns .  � 	Resources 5�      )  #System.Windows.Controls.DataGridRow �� 
TargetType��%E   '        �� 5�      "  6   .�5�      -   5        5�      5�      5�      5�      5�      5�      