   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   M+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5   	   !    DatabaseApp.ProductListUser   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   DuserControls+clr-namespace:DatabaseApp.View.UserControls     5   	     � 
ResizeMode$  NoResize=�5   1    � WindowStartupLocation$ CenterScreen=�5       � Title$ Database��5	   	   $	��500��6   $	��500��6'   .��5
      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��60 �6   �� 5      �� 5      $��35 �6   �� 5      $��50 �6   .+�5   
   � $��0��6   .+�5      +   %DatabaseApp.View.UserControls.MenuBar  5      � 5   
   $��1��6   $��	10,0,10,0q�6   
�����F�.+�5      �� -   5        $��txtList��5      $
��Left=�5      $��Center=�63   
�������$ ��Your list of added product��5   (   $��24(�6J   $��30,0,0,0q�6X   �� 5      -   5        $��
btnRefresh��5      $
��🔄��6'   $��Right=�64   $��0,0,30,0q�6P    � Cursor$
 Hand��5   .   $��25(�6<   $
��auto��6J   $
��auto��6X   
������ 5      5      � 5   
   $��2��6   $��	10,0,10,0q�6   
�����F�.+�5       �� $
��Auto=�6   $��	30,0,30,5q�6>   .��5!      &   System.Windows.Controls.DataGrid  -   5        $��dgList��5!      $��3��6)      AlternatingRowBackground
 ����� �� AlternationCount$ 2��5"         
IsReadOnly .    SelectionMode$ Single=�6B   	   CanUserResizeRows	 .  ��.  $
   HorizontalScrollBarVisibility$
 Disabled=�5$      5%      5&      � 5(   
   $��3��6   $��	10,0,10,0q�6   
�����F� � ColumnDefinitions 5)      �� 5*      $
��0.8* �6#   �� 5+      �� 5,      �� 5-      �� 5.      �� 5/      .+�52      � $��0��6   $��30,0,0,0q�6#   $��Center=�65   .+�53      �� -   5        $��	txtAmount��53      $��0 ��6-   $��Right=�67   
�������$��18(�54   4   55      � 57      $��1��6   $��Center=�6#   .+�58      �� $��
 Products.��6   $
��Left=�6.   
�������$��18(�59   4   5:      � 5<      $��2��6   $��Center=�6#   .+�5=      �� -   5        $��txtItemCount��5=      $��0��60   $��Right=�69   
�������$��18(�5>   4   5?      � 5A      $��3��6   $��Center=�6#   .+�5B      �� $��
  Changed.��6   $
��Left=�6.   
�������$��18(�5C   4   5D      � 5F      $��4��6   $��Center=�6#   .+�5G      �� $��Totals: ��6   $��Right=�6,   
�������$��18(�5H   4   5I      � 5K      $��5��6   $��0,0,30,0q�6#   $��Center=�65   .+�5L      �� -   5        $��txtTotal��5L      $	��0 $��6,   $
��Left=�67   
�������$��18(�5M   4   5N      5O      � 5Q   
   $��4��6   $��
10,0,10,10q�6   
�����F� 5R      �� 5S      �� 5T      .+�5W      � $��0��6   .+�5X      �� $��Center=�6   $��30,0,0,0q�67   �� 5Y      -   5         �Name$ HlBack��5Y   8   
������� � Cursor$
 Hand��5Z       $��16(�6.   .�6<   ↩ Back to Products list5\      5]      5^      � 5`      $��1��6   $��Center=�6#   .+�5a      �� -   5        $��btnClear��5a      $��Clear��6)   $��Right=�69   $��	0,0,120,0q�6U   $
 Hand��5b   1   �� 5c      -	   5        $��	btnSubmit��5c      $��Confirm��6*   $��Right=�6<   $��0,0,30,0q�6X   $
 Hand��5d   1   5e      5f      5g      5h      