   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   M+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5   	   !    DatabaseApp.OrderManagement   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   DuserControls+clr-namespace:DatabaseApp.View.UserControls     5   	     � 
ResizeMode$  NoResize=�5   1    � WindowStartupLocation$ CenterScreen=�5       � Title$ Order Management��5	   	   $	��500��6"   $	��500��6/   .��5
      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��0.15* �6   �� 5      .+�5   
   � $��0��6   .+�5      +   %DatabaseApp.View.UserControls.MenuBar  5      � 5   
   $��1��6   
�����F�$��	10,0,10,0q�63   .+�5      �� $��Order management��6   $��Bottom=�6/   $��5,0,0,0q�6J   
�������$��30(�5   ,   $��Italic$�6:   $��DemiBold"�6M   �� 5      -   5        $��
btnRefresh��5      $
��🔄��6'   $��Right=�64   
������ $��Center=�5      $��0,0,10,0q�60   $��24(�6B   $
��auto��6P   $
��auto��6]    � Cursor$
 Hand��5   .   5      � 5   
   $��2��6   
�����F�$��
10,0,10,10q�63    � ColumnDefinitions 5      �� 5      �� 5       $��0.52* �6#   .+�5#      � $��0��6    5$      �� 5%      $
��0.1* �6$   �� 5&      .+�5)      � $��0��6   $��	10,0,10,0q�6$   .+�5*      �� -   5        $��cbStage��5*      $
��auto��6.   
���   � �� 
IsEditable .  .@�5,      �� $
��Full��6'   �� 5-      $��Stage 0��6'   �� 5.      $��Stage 1��6'   �� 5/      $��Stage 2��6'   �� 50      $��Stage 3��6'   �� 51      $��Stage 4��6'   52      53      � 55      $��1��6   .+�56      &   System.Windows.Controls.DataGrid  -   5        $��dgOrder��56         CanUserAddRows .     CanUserDeleteRows .  $��
10,0,10,10q�6_   	   AlternatingRowBackground
	 �����
 �� AlternationCount$
 2��57   @      
IsReadOnly . $��2��58   -      SelectionMode$ Single=�6A      AutoGenerateColumns . $   HorizontalScrollBarVisibility$
 Auto=�5:      "   VerticalScrollBarVisibility$
 Auto=�6@    � 	Resources 5;      )  #System.Windows.Controls.DataGridRow �� 
TargetType��%E   '        �� 5<      "  6$   .�5=   "   -   5        5>      5?      5@      5A      5B      � 5D      $��1��6    5E      �� 5F      �� 5G      $
��0.6* �6$   .+�5J      � $��0��6   
�������$��0,0,10,0q�6<    5K      �� 5L      $
��0.3* �6(   �� 5M      .+�5P      � $��0��6   
����d� 5Q      �� 5R      $��2* �6/   �� 5S      $	��23* �6/   .+�5U      �� $��Summary��6    $
��Left=�62   $��20(�6M   $��Center=�5V       $��DemiBold"�6;   
�������$��1��5W   8   $��15,0,0,0q�6H   5X      � 5Z      $��1��6   .+�5[      �� $��8,5,8,5q�6%   
�������.+�5\      �� $��Customer name: ��6(   $��15(�6?   �� 5]      -   5        $��txtName��5]   (   $	��...��67   $��14(�6B   
���"�"�6P   �� 5^      $��Payment status: ��6(   $��15(�6@   �� 5_      -   5        $��txtPaymentMethod��5_   (   $	��...��6@   $��14(�6K   
���"�"�6Y   �� 5`      $��Payment method: ��6(   $��15(�6@   �� 5a      -   5        $��	txtStatus��5a   (   $	��...��69   $��14(�6D   
���"�"�6R   �� 5b      $��Order status: ��6(   $��15(�6>   �� 5c      -   5        $��txtStage��5c   (   $	��...��68   $��14(�6C   
���"�"�6Q   5d      5e      5f      � 5h      $��1��6   $��	0,0,10,10q�6$   .+�5i      �� .+�5j      �� -	   5        $��	btnDetail��5j   !   $&��         Order detail            ��62   $
��auto��6]   $��0,10,0,0q�5k   "   $��30��64   $
 Hand��6@   6N   �� 5l      -
   5        $��	btnCancel��5l   !   $!��       Cancel order        ��62   $
��auto��6X   $��0,10,0,0q�5m   !   $��30��63   $
 Hand��6?   6M   �� 5n      -   5        $��	btnUpdate��5n   !   $ ��      Approve order       ��62   $
��auto��6W   $��0,10,0,0q�5o   !   $��30��63   $
 Hand��6?   $y�	Collapsed=�5p   !   5q      5r      5s      5t      5u      5v      