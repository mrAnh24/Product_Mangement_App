   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:DatabaseApp.View.UserControlsDatabaseApp.View.UserControls  5      +    %DatabaseApp.View.UserControls.MenuBar   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:DatabaseApp.View.UserControls     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $��25 �6   �� 5      $��30 �6   .+�5   
   o� .@�5      m� -   5        $��mFile��5      $
��File��6$   $��16(�62   .@�5      m� -   5        $��miHome��5      $
��Home��6)   67   m� 5      -   5        $��	miProduct��5      $��Product��6,   6=   m� 5      -   5        $��LogOut��5      $��Log out��6)   6:   5      m� 5      -   5        $��mAccount��5      $��Account��6'   $��16(�68   .@�5      m� -   5        $��	miAccount��5      $��
My account��6,   6@   m� 5      -   5        $��miProductList��5      $��My products list��60   6J   m� 5      -   5        $��miAccountOrder��5      $��My account order��61   6K   m� 5      -	   5        $��miPermisson��5      $��
Permission��6.   6B   5      m� 5      -
   5        $��mEdit��5      $
��Edit��6$   $��16(�62   .@�5      m� -   5        $��miCSV��5      $��Open .CSV File��6(   6@   m� 5      -   5        $��miUpdateProducts��5      $��Update products��63   6L   m� 5       -   5        $��miUpdateData��5       $��Update data��6/   6D   m� 5!      -   5        $��
ExportData��5!      $��Export data��6-   6B   5"      m� 5$      -   5        $��mAdmin��5$      $��Admin��6%   $��16(�64   .@�5%      m� -   5        $��miInvoiceManagement��5%      $��Invoice Management��66   6R   m� 5&      -   5        $��miOrderManagement��5&      $��Order Management��64   6N   m� 5'      -   5        $��	miRequest��5'      $��Account requests��6,   6F   m� 5(      -   5        $��
miDatabase��5(      $��Account management��6-   6I   m� 5)      -   5        $��miAdminProduct��5)      $��Product management��61   6M   5*      5+      � 5,   
   $��1��6   .+�5-      �� -   5        $��
txtAccname��5-      $��Account name��6*   $��24(�6>   $
��Left=�6L   $
��Bold"�5.      $��Center=�6*   $��10,0,0,0q�6E   �� 5/      -   5        $��btnChat��5/      $
��💬��6$   
����d�$��Right=�6M   
������ $
��auto��50   .   $��0,0,12,0q�6<   $��20(�6N   $
��auto��51   W    � Cursor$
 Hand��52      6#   A� 53      -   5        $��
ChatBubble��53      $��Right=�6(   $��13��6D   $��13��6O   
���  ��$��0,0,8,8q�54   !   $y�	Collapsed=�62   �� 55      -   5        $��btnBell��55      $
��🔔��6$   
����d�$��Right=�6N   
������ $
��auto��56   .   $��0,0,50,0q�6<   $��20(�6N   $
��auto��6\   $
 Hand��58      6#   A� 59      -   5        $��NotifyBubble��59      $��Right=�6*   $��13��6F   $��13��6Q   
���  ��$��0,0,48,8q�5:   !   $y�	Collapsed=�63   �� 5;      -   5        $��
NotifyText��5;      $��Right=�6*   
�������$��Center=�5<      $��0,5,75,5q�63   5=      5>      5?      