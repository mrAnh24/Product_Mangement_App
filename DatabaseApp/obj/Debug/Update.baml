   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   M+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5   	       DatabaseApp.Update   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   DuserControls+clr-namespace:DatabaseApp.View.UserControls     5   	     � 
ResizeMode$  NoResize=�5   1    � WindowStartupLocation$ CenterScreen=�5       � Title$ Products Update��5	   	   $	��500��6!   $	��500��6.   .��5
      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��45 �6   �� 5      �� 5      $��40 �6   �� 5      �� 5      $��10 �6   .+�5   
   � $��0��6   .+�5      +   %DatabaseApp.View.UserControls.MenuBar  $�� 0,0,0,5q�6#   5      � 5   
   $��1��6   $��	10,0,10,0q�6   
�����F�.+�5      �� $��Update Products data��6   
�������$��Center=�5      $��Center=�63   $��30(�6N   $��1��6\   $��10,0,0,0q�6l   5      � 5   
   $��2��6   $��	10,0,10,0q�6   
�����F� � ColumnDefinitions 5      �� 5      $	��130 �6#   �� 5       �� 5!      $	��130 �6#   .+�5$      � $��0��6   $��20,0,0,0q�6#   .+�5%      �� $��Center=�6   .+�5&      �� -   5        $��txtProductCode��5&       $��Product Code:��66   $��16(�6K   
�������$��Bottom=�5'   2   $��0,0,0,10q�6M   �� 5(      -   5        $��
txtProduct��5(       $��Product:��62   $��16(�6B   
�������$��Center=�5)   1   $��0,0,0,10q�6L   �� 5*      -   5        $��txtDescription��5*       $��Description:��66   $��16(�6J   
�������$��Center=�5+   1   $��0,0,0,10q�6L   �� 5,      -   5        $��Price��5,       $��Price:��6-   $��16(�6;   
�������$	��Top=�5-   1   $��0,0,0,10q�6I   5.      5/      � 51      $��1��6   .+�52      �� $��Center=�6   .+�53      �� -   5        $��tbProductId��53      $��25��61   $��0,0,10,8q�6=   $��16(�6O   54      �� 55      -   5        $��	tbProduct��55      $��25��6/   $��0,0,10,8q�6;   $��16(�6M   56      �� 57      -   5        $��tbDescription��57      $��25��63   $��0,0,10,8q�6?   $��16(�6Q   �� 58      -   5        $��tbPrice��58      $��25��6-   $��0,0,10,8q�69   $��16(�6K   6Y   59      5:      � 5<      $��2��6   $��0,0,20,0q�6$   .+�5=      �� $	��100��6   $��D://cable.png��6%   5>      5?      � 5A   
   $��3��6   $��	10,0,10,0q�6   
�����F�.+�5B      �� -	   5        $��btnAdd��5B      $	��Add��6#   $
��Left=�61   $��	25,0,0,10q�6L    � Cursor$
 Hand��5C   *   �� 5D      -
   5        $��	btnUpdate��5D      $��Update��6&   $
��Left=�67   $��
114,0,0,10q�6R   $
 Hand��5E   -   �� 5F      -   5        $��	btnDelete��5F      $��Delete��6&   $��0,0,0,10q�67   $
 Hand��5G   -   �� 5H      -   5        $��
btnRefresh��5H      $��Refresh��6'   $��Right=�69   $��
0,0,114,10q�6U   $
 Hand��5I   .   �� 5J      -   5        $��btnClear��5J      $��Clear��6%   $��Right=�65   $��	0,0,25,10q�6Q   $
 Hand��5K   ,   5L      � 5N   
   $��4��6   $��	10,0,10,0q�6   
�����F�.+�5O      &   System.Windows.Controls.DataGrid  -   5        $��	dgProduct��5O      $��
20,0,20,20q�6(      
IsReadOnly .    SelectionMode$ Single=�6N   $��3��5P         AlternatingRowBackground
 �����	 �� AlternationCount$	 2��6P   
   CanUserResizeRows
 .   � 	Resources 5R      )  #System.Windows.Controls.DataGridRow �� 
TargetType��%E   '        �� 5S      "  6   .�5T      -   5        5U      5V      5W      5X      5Y      5Z      