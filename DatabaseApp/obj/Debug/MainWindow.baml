   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   M+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5   	       DatabaseApp.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   DuserControls+clr-namespace:DatabaseApp.View.UserControls     5   	     � 
ResizeMode$  NoResize=�5   1    � WindowStartupLocation$ CenterScreen=�5       � Title$ Main Window��5	   	   $	��500��6   $	��500��6*   .��5
      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��45 �6   �� 5      �� 5      $��40 �6   �� 5      $��10 �6   .+�5   
   � $��0��6   .+�5      +   %DatabaseApp.View.UserControls.MenuBar  $�� 0,0,0,5q�6#   5      � 5   
   $��1��6   $��	10,0,10,0q�6   
�����F� � ColumnDefinitions 5      �� 5      �� 5      $	��155 �6#   .+�5      � $��0��6   .+�5      �� -   5        $��tbSearchBox��5      $��20(�6-   $��30��6;   $
��auto��6G   $��10,0,5,0q�6T   �� 5      -   5         ��Name$ btnSearchClear��5      $��20��61   $��Right=�6<   $��X��6X   
�������
������ $��0q�5    I   $��8q�5!   6   5"      � 5$      $��1��6   .+�5%      �� -   5        $��	btnSearch��5%      $��Search��6*   $��Right=�6;   $��0,0,85,0q�5&       � Cursor$
 Hand��6C   �� 5'      -   5        $��cbFilter��5'      $��Right=�6+    �� 
IsReadOnly .  �� 
IsEditable .  $��0,0,10,0q�5(      
���   �.@�5)      �� $��by name��6#   �� 5*      $��by code��6$   �� 5+      $��by price��6$   5,      5-      5/      � 51   
   $��2��6   $��	10,0,10,0q�6   
�����F� 52      �� 53      $
��457* �6#   �� 54      $	��23* �6#   .+�56      &   System.Windows.Controls.DataGrid  -   5        	  Name$	 dgExcel��56      
   CanUserAddRows
 .     CanUserDeleteRows .  $��10,10,10,10q�6Y      AlternatingRowBackground
 ����� �� AlternationCount$ 2��57   <      
IsReadOnly . $��2��58   )   59      � 5;   
   $��3��6   $��	10,0,10,0q�6   
�����F�.+�5<      �� -   5        $��txtTotal��5<      $��Total record: ...��6(   $��20(�6A   
�������$��Center=�5=      $��	10,0,0,10q�63   �� 5>      -   5        $��btnClear��5>      $
��🔄��6%   $��Right=�62   $��
0,-10,90,0q�6N   $
 Hand��5?   ,   $
��auto��6:   $
��auto��6H   $��20(�6U   
������ 5@      �� 5A      -   5        $��btnOpen��5A      $��Right=�6$   $
��📁��6@   $��	0,0,10,10q�6M   $
 Hand��5B   +   5C      5D      5E      