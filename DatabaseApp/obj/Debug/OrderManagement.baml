   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   M+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5   	   !    DatabaseApp.OrderManagement   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   DuserControls+clr-namespace:DatabaseApp.View.UserControls     5   	     � 
ResizeMode$  NoResize=�5   1    � WindowStartupLocation$ CenterScreen=�5       � Title$ Order Management��5	   	   $	��500��6"   $	��500��6/   .��5
      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��0.15* �6   �� 5      .+�5   
   � $��0��6   .+�5      +   %DatabaseApp.View.UserControls.MenuBar  5      � 5   
   $��1��6   
�����F�$��	10,0,10,0q�63   .+�5      �� $��Order management��6   $��Bottom=�6/   $��5,0,0,0q�6J   
�������$��30(�5   ,   $��Italic$�6:   $��DemiBold"�6M   5      � 5   
   $��2��6   
�����F�$��
10,0,10,10q�63    � ColumnDefinitions 5      �� 5      �� 5      $��0.52* �6#   .+�5       � $��0��6    5!      �� 5"      $
��0.1* �6$   �� 5#      .+�5&      � $��0��6   $��	10,0,10,0q�6$   .+�5'      �� -   5        $��cbStage��5'      $
��auto��6.   
���   � �� 
IsEditable .  .@�5)      �� $
��Full��6'   �� 5*      $��Stage 0��6'   �� 5+      $��Stage 1��6'   �� 5,      $��Stage 2��6'   �� 5-      $��Stage 3��6'   �� 5.      $��Stage 4��6'   5/      50      � 52      $��1��6   .+�53      &   System.Windows.Controls.DataGrid  -   5        $��dgOrder��53         CanUserAddRows .     CanUserDeleteRows .  $��
10,0,10,10q�6_      AlternatingRowBackground
 �����	 �� AlternationCount$	 2��54   @   
   
IsReadOnly
 . $��2��55   -      SelectionMode$ Single=�6A      AutoGenerateColumns . $   HorizontalScrollBarVisibility$
 Auto=�57      "   VerticalScrollBarVisibility$
 Auto=�6@    � 	Resources 58      )  #System.Windows.Controls.DataGridRow �� 
TargetType��%E   '        �� 59      "  6$   .�5:   "   -   5        5;      5<      5=      5>      5?      � 5A      $��1��6    5B      �� 5C      �� 5D      $
��0.6* �6$   .+�5G      � $��0��6   
�������$��0,0,10,0q�6<    5H      �� 5I      $
��0.3* �6(   �� 5J      .+�5M      � $��0��6   
����d� 5N      �� 5O      $��2* �6/   �� 5P      $	��23* �6/   .+�5R      �� $��Summary��6    $
��Left=�62   $��20(�6M   $��Center=�5S       $��DemiBold"�6;   
�������$��1��5T   8   $��15,0,0,0q�6H   5U      � 5W      $��1��6   .+�5X      �� $��8,5,8,5q�6%   
�������.+�5Y      �� $��Customer name: ��6(   $��15(�6?   �� 5Z      -   5        $��txtName��5Z   (   $	��...��67   $��14(�6B   
���"�"�6P   �� 5[      $��Payment status: ��6(   $��15(�6@   �� 5\      -   5        $��txtPaymentMethod��5\   (   $	��...��6@   $��14(�6K   
���"�"�6Y   �� 5]      $��Payment method: ��6(   $��15(�6@   �� 5^      -   5        $��	txtStatus��5^   (   $	��...��69   $��14(�6D   
���"�"�6R   �� 5_      $��Order status: ��6(   $��15(�6>   �� 5`      -   5        $��txtStage��5`   (   $	��...��68   $��14(�6C   
���"�"�6Q   5a      5b      5c      � 5e      $��1��6   $��	0,0,10,10q�6$   .+�5f      �� .+�5g      �� -   5        $��	btnDetail��5g   !   $&��         Order detail            ��62   $
��auto��6]   $��0,10,0,0q�5h   "   $��30��64    � Cursor$
 Hand��6@   6N   �� 5i      -	   5        $��	btnUpdate��5i   !   $�� Change order status  ��62   $
��auto��6S   $��0,10,0,0q�5j   "   $��30��64   $
 Hand��6@   6N   5k      �� 5l      -
   5        $��
btnRefresh��5l      $
��🔄��6/   $��Right=�6<   
������ $��Bottom=�5m      $��0,0,0,0q�68   $��24(�6I   $
��auto��6W   $
��auto��6d   $
 Hand��5n   6   5o      5p      5q      5r      5s      