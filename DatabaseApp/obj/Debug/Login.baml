   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	       DatabaseApp.Login   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	   -   5          � WindowStyle$
  None=�5       � WindowStartupLocation$ CenterScreen=�6-    � 
ResizeMode$ NoResize=�5   :    � Title$ Login��5	   	   $	��420��6   $	��280��6$   '  !System.Windows.Shell.WindowChrome   WindowChrome 5
        5   
      GlassFrameThickness$ 0q�6      CornerRadius$ 0��6/      CaptionHeight$ 0Q�6@   .��5      � 
������ � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��60 �6   �� 5      �� 5      $��60 �6   �� 5      $��30 �6   �� 5      $��60 �6   .+�5   
   � $��0��6   .+�5      �� -   5        $��txtClose��5      $��X��6$   $��Right=�60   $��24(�6L   	 � Cursor$
	 Hand��6Z   $��Center=�5      $��0,0,10,0q�63   6E   5      � 5   
   $��1��6   .+�5      �� $��Login��6   $��30(�6$   $��Center=�62   $��Center=�5      5       � 5"   
   $��2��6   .+�5#      �� $��Center=�6   $��	50,0,50,0q�64   .+�5$      �� -   5        $��txtUsername��5$      $��	Username:��6/   $��15(�6@   �� 5%      -   5        $��
tbUsername��5%      $��15(�6,   $��24��6:   $��0,0,0,5q�6F   �� 5&      -   5        $��txtPassword��5&      $��	Password:��6/   $��15(�6@   �� 5'      -   5        $��
tbPassword��5'      $��15(�6,   $��24��6:   $��0,0,0,5q�6F   G� 5(      -   5        $��
pbPassword��5(      $��15(�60   $��24��6>   $��	0,-29,0,5q�6J   �� 5)      -   5        $��
passwordCb��5)      $��Show password��6-   $��15(�6E   $��24��6S   5*      5+      5,      � 5.   
   $��3��6   .+�5/      �� -	   5        $��btnLogIn��5/      $��Login��6%   $��20(�65   $��40��6C   $	��100��6O   $��Center=�50   ,   $
	 Hand��6I   51      � 53   
   $��4��6   .+�54      �� $��Don't have an account?��6   $��
60,0,10,10q�66   �� 55      $��190,0,10,10q�6   �� 56      -
   5        
 �Name$
 txtRegister��56       � Cursor$
 Hand��6I   .�6W   	Sign up6`   57      58      � 5:   
   $��5��6   $��Center=�6   .+�5;      �� $	��or ��6   $��10,-6,10,10q�6#   �� 5<      $��26,-6,10,10q�6   �� 5=      -   5        $
 hlGuest��5=      $
 Hand��6B   .�6P   sign in as Guest6b   5>      5?      5@      5A      