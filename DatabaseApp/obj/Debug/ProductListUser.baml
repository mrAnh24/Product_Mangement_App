   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	   M+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5   	   !    DatabaseApp.ProductListUser   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   DuserControls+clr-namespace:DatabaseApp.View.UserControls     5   	     � WindowStartupLocation$  CenterScreen=�5       � Title$ Database��5	   	   $	��500��6   $	��500��6'   .��5
      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��60 �6   �� 5      �� 5      $��60 �6   .+�5   
   � $��0��6   .+�5      +   %DatabaseApp.View.UserControls.MenuBar  5      � 5   
   $��1��6   $��	10,0,10,0q�6   
�����F�.+�5      �� $��Center=�6   $��Center=�65   
�������$ ��Your list of added product��5      $��24(�6:   5      � 5   
   $��2��6   $��	10,0,10,0q�6   
�����F�.+�5      �� $
��Auto=�6   $��
30,10,30,5q�6>   .��5      �� $��10,0,0,0q�6   
�����_���5      �� 5       .R�5!      ��  �� Padding$ 10q�6%   
���   �$��1q�6F   ._�5"   "   �  5#   &   �� 5$   *   $
��auto �68   �� 5%   *   $
��auto �68   �� 5&   *   $
��auto �68   .+�5)   &   �� $��0��60   ��6=   �� *	Product+�� 5*   &   $��1��60   ��6=   �� *ProductCode+�� 5+   &   $��2��60   ��6=   �� *Price+ �� StringFormat$ {0:C}��5,   #   5-      5.      50      51      52      � 54   
   $��3��6   $��
10,0,10,10q�6   
�����F� � ColumnDefinitions 55      �� 56      �� 57      .+�5:      � $��0��6   .+�5;      �� -   5        $��	btnReturn��5;      $��Back to Product��6*   $	��120��6D   $��10,0,0,0q�6P   $
��Left=�5<      64   �� 5=      -   5        $��btnClear��5=      $��Clear��6)   $��Right=�69   $��0,0,10,0q�6U   5>      5?      � 5A      $��1��6   $��Center=�6#   .+�5B      �� $��Total:��6   
�������$��60,0,0,0q�6B   $��18(�6T   �� 5C      -   5        $��txtTotal��5C      $�� ��6,   
�������$��Right=�6L   $��0,0,30,0q�5D      $��18(�6/   5E      5F      5G      5H      