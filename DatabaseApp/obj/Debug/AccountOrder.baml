   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   M+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5   	       DatabaseApp.AccountOrder   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   DuserControls+clr-namespace:DatabaseApp.View.UserControls     5   	     � WindowStartupLocation$  CenterScreen=�5       � Title$ Account Order��5	   	   $	��500��6   $	��500��6,    � 
ResizeMode$ NoResize=�68   .��5
      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      .+�5   
   � $��0��6   .+�5      +   %DatabaseApp.View.UserControls.MenuBar  5      � 5   
   $��1��6   $��
10,0,10,10q�6   
�����F�.+�5      � $��0��6   $��	10,0,10,0q�6     5      �� 5      $��0.15* �6$   �� 5      $��0.10* �6$   �� 5      .+�5      � $��0��6   .+�5      �� -   5        $��txtTitle��5       $��List of ... orders��60   
�������$��30(�6b   $��DemiBold"�5       $��Center=�66   5      � 5!      $��1��6    � ColumnDefinitions 5"      �� 5#      �� 5$      $
��0.2* �6+   .+�5'      � $��0��6   .+�5(      �� $��25��6"   $��16(�6.   5)      � 5+      $��1��6   .+�5,      �� -   5        $��	btnSearch��5,   !   $��Search��62   5-      5.      � 50      $��2��6   $��	0,10,0,10q�6$    51      �� 52      �� 53      $
��0.4* �6+   .+�56      � $��0��6   .+�57      &   System.Windows.Controls.DataGrid  -   5        $��	dgInvoice��57   #      CanUserAddRows .     CanUserDeleteRows .  $��0,0,10,0q�6e      AlternatingRowBackground
 ����� �� AlternationCount$ 2��58   H   	   
IsReadOnly	 . $��2��59   5   
   SelectionMode$
 Single=�6I      AutoGenerateColumns . $   HorizontalScrollBarVisibility$
 Auto=�5;   #   "   VerticalScrollBarVisibility$
 Auto=�6H    � 	Resources 5<      )  #System.Windows.Controls.DataGridRow �� 
TargetType��%E   '        �� 5=   "   "  6(   .�5>   &   -   5        5?   #   5@      5A      5B      � 5D      $��1��6    5E      �� 5F      �� 5G      �� 5H      $
��8.0* �6,   .+�5K      � $��0��6   .+�5L      �� -   5        $��cbFilter��5L   '   
���   � �� 
IsEditable .  $
��auto��6]   .@�5N   "   �� $	��All��6/   �� 5O   "   $��Ongoing��6/   �� 5P   "   $��	Completed��6/   �� 5Q   "   $��Canceled��6/   5R      5S      � 5U      $��1��6   .+�5V      �� -   5        $��cbOrder��5V   '   
���   �$
��auto��6I   {�.   .  .@�5X   "   �� $
��Bill��6/   �� 5Y   "   $
��Date��6/   5Z      5[      � 5]      $��2��6   $��0,10,0,0q�6,   .+�5^      �� .+�5_   "   �� -   5        $��btnOderDetail��5_   )   $	��140��6>   $��30��6J   $��0,0,0,10q�6V   $��Order detail��5`   )    � Cursor$
 Hand��6\   �� 5a   "   -   5        $��btnProductsDetail��5a   )   $	��140��6B   $��30��6N   $��0,0,0,10q�6Z   $��Products detail��5b   )   $
 Hand��6c   �� 5c   "   -	   5        $��btnOderStatus��5c   )   $	��140��6>   $��30��6J   $��0,0,0,10q�6V   $��Order status��5d   )   $
 Hand��6\   �� 5e   "   -
   5        $��btnCancelOder��5e   )   $	��140��6>   $��30��6J   $��0,0,0,10q�6V   $��Cancel order��5f   )   $
 Hand��6\   5g      �� 5h      -   5        $��
btnAccount��5h   %   $
��👤��67   $
��Left=�6D   $��20,0,0,0q�6_   $
 Hand��5i   ?   $
��auto��6M   $
��auto��6[   $��24(�6h   
������ $��Bottom=�5j   >   �� 5k      -   5        $��
btnRefresh��5k   %   $
��🔄��67   $��Right=�6D   $��24(�6`   $
��auto��5l   %   $
 Hand��6K   $
��auto��6Y   
������ $��Bottom=�5m   >   $��0,0,40,0q�6Y   �� 5n      -   5        $��btnRefreshbtnHelp��5n   %   $	��❓��6>   $��Right=�6J   $��24(�6f   $
��auto��5o   %   $
 Hand��6K   $
��auto��6Y   
������ $��Bottom=�5p   >   $��0,0,10,0q�6Y   5q      5r      5s      5t      5u      5w      5x      