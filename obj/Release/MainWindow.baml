   M S B A M L   `   `   `  ���� B  >TSP_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TSP_WPFTSP_WPF      TSP_WPF.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:TSP_WPF     -     �Name$  MyWindow�� � Title$ 
MainWindow��$	��500��$
��1000��.��� .+��� -   $��Start!��$
��Left=�$��35��$��834,272,0,0q�$	��Top=�$	��111���� -    ��Name$	 CCC��$�� ��$
��Left=�$��	10,11,0,0q�$	��Top=�$	��299��$	��367���� $
��Bold"�$��781,215,-261.4,203.4q��� $�Cooling���� �� $�Speed:���� -    ��Name$ speed��$��32��$
��left=�$��885,215,0,0q�$	��Top=�$��60��$
��0.98���� -   $ Init_Temperature��$��32��$
��Left=�$	��Top=�$��60��$��885,138,0,0q�$	��100���� $��InitialTemperature:��$
��Bold"�$��781,138,-261.4,280.4q��� -   $ Min_Temperature��$��60��$��32��$��
885,69,0,0q�$
��Left=�$	��Top=�$��0.001���� $
��Bold"�$��781,69,113.6,349.4q��� $	�Min���� �� $�Temperature:���� -   $ Result��$	��210��$��309,337,0,0q�$
��Left=�$	��Top=�$	��122���� -    ��Name$
 Best��$
��Left=�$��177,321,0,0q�$	��Top=�$y�Hidden=����� *
runtimes+�� $
��Left=�$��
10,315,0,0q�$
��Wrap=�$	��Top=�$��93��$y�Hidden=��� $�Current���� $� AS&HC���� $�:���� $
��Left=�$��
10,349,0,0q�$
��Wrap=�$��AS Current:��$	��Top=�$��74���� -	   $ AS_Best��$
��Left=�$��177,354,0,0q�$	��Top=����� *
runtimes+�� $
��Left=�$��
10,384,0,0q�$
��Wrap=�$	��Top=�$��74��$��35���� $�HillClimbing Current:���� �� �� -
   $ HillClimbing_Best��$
��Left=�$��177,395,0,0q�$	��Top=����� *
runtimes+�� -   $ GA_Best��$
��Left=�$��177,424,0,0q�$	��Top=� J� RenderTransformOrigin$ 0.633,-2.067&����� *
runtimes+�� -   $ CCC_Copy��$�� ��$
��Left=�$��
365,11,0,0q�$	��Top=�$	��299��$	��367���� $
��Left=�$��
10,424,0,0q�$
��Wrap=�$	��Top=�$��74��$��35���� $�GA Current:���� �� �� -   $ 	CCC_Copy1��$��ContentControl��$
��Left=�$��664,333,0,0q�$	��Top=�$	��126��$	��154���� $
��Left=�$��570,333,0,0q�$
��Wrap=�$	��Top=��� $
�Opt ���� $�Graph��