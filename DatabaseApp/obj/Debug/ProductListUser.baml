   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   M+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5   	   !    DatabaseApp.ProductListUser   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   DuserControls+clr-namespace:DatabaseApp.View.UserControls     5   	     � 
ResizeMode$  NoResize=�5   1    � WindowStartupLocation$ CenterScreen=�5       � Title$ Product List User��5	   	   $	��500��6#   $	��500��60   .��5
      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��60 �6   �� 5      �� 5      $��60 �6   .+�5   
   � $��0��6   .+�5      +   %DatabaseApp.View.UserControls.MenuBar  5      � 5   
   $��1��6   $��	10,0,10,0q�6   
�����F�.+�5      �� -   5        $��txtList��5      $
��Left=�5      $��Center=�63   
�������$ ��Your list of added product��5   (   $��24(�6J   $��30,0,0,0q�6X   �� 5      -   5        $��
btnAccount��5      $
��👤��6'   $��Right=�64   $��0,0,50,0q�6P    � Cursor$
 Hand��5   /   $
��auto��6=   $
��auto��6K   $��24(�6X   
������ 5      �� 5      -   5        $��
btnRefresh��5      $
��🔄��6'   $��Right=�64   $��0,0,10,0q�6P   $
 Hand��5   .   $��24(�6<   $
��auto��6J   $
��auto��6X   
������ 5      5      � 5!   
   $��2��6   $��	10,0,10,0q�6   
�����F� � ColumnDefinitions 5"      �� 5#      �� 5$      $��0.45* �6#   .+�5'      � $��0��6   .+�5(      �� $
��Auto=�6   $��15,0,0,0q�6B   .��5)      &   System.Windows.Controls.DataGrid  -   5        $��dgList��5)      $��3��6-      AlternatingRowBackground
 ����� �� AlternationCount$ 2��5*         
IsReadOnly . 	   SelectionMode$	 Single=�6F   
   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 * 6]      CanUserResizeRows .  ��.  $   HorizontalScrollBarVisibility$ Disabled=�5,       � 	Resources 5-      )  #System.Windows.Controls.DataGridRow �� 
TargetType��%E   '        �� 5.      "  6$   .�5/   "   -   5        50      51      52      53      54      � 56      $��1��6   $��5,0,15,0q�6#    57      �� 58      �� 59      $
��0.6* �6$   .+�5<      � $��0��6   $��5,0,0,5q�6$   
������� 5=      �� 5>      $
��0.7* �6(   �� 5?      �� 5@      $��0.01* �6(   �� 5A      $
��0.5* �6(   .+�5D      � $��0��6   
����d�.+�5E      �� $��Detail��6    $��Center=�61   $��Center=�6N   $��24(�5F       
�������6.   5G      � 5I      $��1��6   $��5,10,5,0q�6(    5J      �� 5K      $
��0.6* �6/   �� 5L      .+�5O      � $��0��6   .+�5P      �� $��0,0,5,0q�6)   .+�5Q   "   �� -   5        $��	txtAmount��5Q   ,   $��0��6=   $��14(�6F   $��Right=�5R   ,   $��0,0,0,5q�6H   �� 5S   "   -   5        $��txtItemCount��5S   ,   $��0��6@   $��14(�6I   $��Right=�5T   ,   $��0,0,0,5q�6H   5U      5V      � 5X      $��1��6   $��2,0,0,0q�6/   .+�5Y      �� .+�5Z   "   �� $��	Products.��6,   $
��Left=�6=   $��14(�5[   ,   $��0,0,0,5q�6:   �� 5\   "   $��Changed.��6,   $
��Left=�6<   $��14(�5]   ,   $��0,0,0,5q�6:   5^      5_      5`      � 5b      $��2��6   
����d�6(   � 5d      $��3��6   $��	5,10,5,10q�6(    5e      �� 5f      $
��0.7* �6/   �� 5g      .+�5j      � $��0��6   $��5,0,0,0q�6/   .+�5k      �� $��Totals: ��6(   $��14(�68   5l      � 5n      $��1��6   $��5,0,0,0q�6/   .+�5o      �� -   5        $��txtTotal��5o   (   $	��0 $��68   $
��Left=�6C   $��14(�5p   (   5q      5r      5s      � 5u      $��1��6   .+�5v      �� $��5,10,0,0q�6!   $��Stretch=�63   .+�5w      �� -	   5        $��	btnUpdate��5x   !   $"��        Update              ��5w   !   $��0,0,0,0q�6H   $
 Hand��5x   2   $
��auto��6@   �� 5y      -
   5        $��	btnDelete��5z   !   $"��         Delete             ��5y   !   $��0,10,0,0q�6H   $
 Hand��5z   2   $
��auto��6@   �� 5{      -   5        $��btnClear��5|   !   $"��          Clear             ��5{   !   $��0,10,0,0q�6H   $
��auto��5|   1   $
 Hand��6>   5}      5~      5      5�      � 5�   
   $��3��6   $��
10,0,10,10q�6   
�����F� 5�      �� 5�      �� 5�      .+�5�      � $��0��6   .+�5�      �� $��Center=�6   $��15,0,0,0q�67   �� 5�      -   5         �Name$ HlBack��5�   8   
������� � Cursor$
 Hand��5�       $��20(�6.   .�6<   ↩ Back to Products list5�      5�      5�      � 5�      $��1��6   $��Center=�6#   .+�5�      �� -   5        $��	btnSubmit��5�      $�� ╰┈Checkout ┈➤ ��6*   $��Right=�6D   $��0,0,15,0q�6`   $
 Hand��5�   1   $
��auto��6?   5�      5�      5�      5�      