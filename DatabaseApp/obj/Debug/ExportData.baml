   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   M+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5   	       DatabaseApp.ExportData   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   DuserControls+clr-namespace:DatabaseApp.View.UserControls     5   	     � 
ResizeMode$  NoResize=�5   1    � WindowStartupLocation$ CenterScreen=�5       � Title$ Export Data��5	   	   $	��500��6   $	��500��6*   .��5
      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      �� 5      $��10 �6   .+�5   
   � $��0��6   .+�5      +   %DatabaseApp.View.UserControls.MenuBar  5      � 5   
   $��1��6   
�����F�$��	10,0,10,0q�63    5      �� 5      �� 5      $��90 �6    �� 5      $��50 �6    .+�5      � $��0��6   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��dgExport��5      $��
15,20,15,0q�6+   6A   5      � 5       $��1��6    � ColumnDefinitions 5!      �� 5"      $��1* �6'   �� 5#      $��4* �6'   �� 5$      $
��0.5* �6'   .+�5'      � $��0��6   $��Center=�6'   $��Center=�6D   .+�5(      �� $��10,6,0,0q�6!   .+�5)      �� $��Name :��6    $��18(�61   
�������6?   �� 5*      $��	Default :��6    $��18(�64   
�������6B   5+      5,      � 5.      $��1��6   .+�5/      �� .+�50      �� -   5        $��tbName��50   "   $��Center=�60   $��	0,20,30,0q�6K   $��14(�51   "   $��24��60   �� 52      -   5        $	��cbV��52   #   $
��Left=�6.   $��0,12,0,0q�6I   $��80��53   #   54      55      � 57      $��2��6   .+�58      �� -   5        $��btnLoad��58      $
��📁��6,   $��-25,0,15,25q�69    � Cursor$
 Hand��6N   59      5:      � 5<      $��2��6   .+�5=      �� -   5        $��	btnExport��5=      $��Export��6*   $��Right=�6;   $��	0,0,105,0q�5>      $
 Hand��6,   �� 5?      -   5        $��btnClear��5?      $��Clear��6)   $��Right=�69   $��0,0,15,0q�5@      $
 Hand��6+   5A      5B      5D      5E      