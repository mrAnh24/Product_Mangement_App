   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   M+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5   	   !    DatabaseApp.OrderManagement   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   DuserControls+clr-namespace:DatabaseApp.View.UserControls     5   	     � 
ResizeMode$  NoResize=�5   1    � WindowStartupLocation$ CenterScreen=�5       � Title$ Order Management��5	   	   $	��500��6"   $	��500��6/   .��5
      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $	��33* �6   �� 5      $	��22* �6   �� 5      $
��369* �6   .+�5   
   � $��0��6   .+�5      +   %DatabaseApp.View.UserControls.MenuBar  5      � 5   
   $��1��6   
�����F�$��	10,0,10,0q�63   $��2��6F   .+�5      �� $��Order management��6   $��Bottom=�6/   $��5,0,0,0q�6J   
�������$��30(�5   ,   $��Italic$�6:   $��DemiBold"�6M   �� 5      -   5        $��
btnRefresh��5      $
��🔄��6'   $��Right=�64   
������ $��Center=�5      $��0,0,10,0q�60   $��24(�6B   $
��auto��6P   $
��auto��6]    � Cursor$
 Hand��5   .   5      � 5   
   $��2��6   
�����F�$��10,22,10,10q�63   $��2��6H    � ColumnDefinitions 5      �� 5       �� 5!      $��0.52* �6#   .+�5$      � $��0��6    5%      �� 5&      $
��0.1* �6$   �� 5'      .+�5*      � $��0��6   $��	10,0,10,0q�6$   .+�5+      �� -   5        $��cbStage��5+      $
��auto��6.   
���   � �� 
IsEditable .  .@�5-      �� $
��Full��6'   �� 5.      $��Stage 0��6'   �� 5/      $��Stage 1��6'   �� 50      $��Stage 2��6'   �� 51      $��Stage 3��6'   �� 52      $��Stage 4��6'   53      54      � 56      $��1��6   .+�57      &   System.Windows.Controls.DataGrid  -   5        $��dgOrder��57         CanUserAddRows .     CanUserDeleteRows .  $��
10,0,10,10q�6_   	   AlternatingRowBackground
	 �����
 �� AlternationCount$
 2��58   @      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6U      
IsReadOnly . $��2��59   -      SelectionMode$ Single=�6A      AutoGenerateColumns . $   HorizontalScrollBarVisibility$
 Auto=�5;      "   VerticalScrollBarVisibility$
 Auto=�6@    � 	Resources 5<      )  #System.Windows.Controls.DataGridRow �� 
TargetType��%E   '        �� 5=      "  6$   .�5>   "   -   5        5?      5@      5A      5B      5C      � 5E      $��1��6    5F      �� 5G      �� 5H      $
��0.6* �6$   .+�5K      � $��0��6   
�������$��0,0,10,0q�6<    5L      �� 5M      $
��0.3* �6(   �� 5N      .+�5Q      � $��0��6   
����d� 5R      �� 5S      $��2* �6/   �� 5T      $	��23* �6/   .+�5V      �� $��Summary��6    $
��Left=�62   $��20(�6M   $��Center=�5W       $��DemiBold"�6;   
�������$��1��5X   8   $��15,0,0,0q�6H   5Y      � 5[      $��1��6   .+�5\      �� $��8,5,8,5q�6%   
�������.+�5]      �� $��Customer name: ��6(   $��15(�6?   �� 5^      -   5        $��txtName��5^   (   $	��...��67   $��14(�6B   
���"�"�6P   �� 5_      $��Payment method: ��6(   $��15(�6@   �� 5`      -   5        $��txtPaymentMethod��5`   (   $	��...��6@   $��14(�6K   
���"�"�6Y   �� 5a      $��Payment status: ��6(   $��15(�6@   �� 5b      -   5        $��	txtStatus��5b   (   $	��...��69   $��14(�6D   
���"�"�6R   �� 5c      $��Order status: ��6(   $��15(�6>   �� 5d      -   5        $��txtStage��5d   (   $	��...��68   $��14(�6C   
���"�"�6Q   5e      5f      5g      � 5i      $��1��6   $��	0,0,10,10q�6$   .+�5j      �� .+�5k      �� -	   5        $��	btnDetail��5k   !   $&��         Order detail            ��62   $
��auto��6]   $��0,10,0,0q�5l   "   $��30��64   $
 Hand��6@   6N   �� 5m      -
   5        $��	btnCancel��5m   !   $!��       Cancel order        ��62   $
��auto��6X   $��0,10,0,0q�5n   !   $��30��63   $
 Hand��6?   6M   �� 5o      -   5        $��	btnUpdate��5o   !   $ ��      Approve order       ��62   $
��auto��6W   $��0,10,0,0q�5p   !   $��30��63   $
 Hand��6?   $y�	Collapsed=�5q   !   �� 5r      -   5        $��btnPrint��5r   !   $��Print all order��61   $
��auto��6K   $��0,10,0,0q�5s   !   $��30��63   $
 Hand��6?   $y�	Collapsed=�6M   5t      5u      5v      5w      5x      5y      