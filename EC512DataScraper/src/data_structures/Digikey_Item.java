package data_structures;

public class Digikey_Item {
	public String ID;
	public String manufacturer;
	public String description;
	public int quantity;
	public String availability;
	public double price;
	public int minimum_quantity;
	public String packaging;
	public String series;
	public double resistance_ohms;
	public int tolerance;
	public double power_watts;
	
	public String print(){
		String str = "";
		str = String.format("%s\nID = %s\n",str, ID);
		str = String.format("%s\nmanufacturer = %s\n",str, manufacturer);
		str = String.format("%s\ndescription = %s\n",str, description);
		str = String.format("%s\nquantity = %d\n",str, quantity);
		str = String.format("%s\navailability = %s\n",str, availability);
		str = String.format("%s\nprice = $%f\n",str, price);
		str = String.format("%s\nminimum_quantity = %d\n",str, minimum_quantity);
		str = String.format("%s\npackaging = %s\n",str, packaging);
		str = String.format("%s\nseries = %s\n",str, series);
		str = String.format("%s\nresistance_ohms = %f ohms\n",str, resistance_ohms);
		str = String.format("%s\ntolerance = %d\n",str, tolerance);
		str = String.format("%s\npower_watts = %f W\n",str, power_watts);
		return str;
	}
	
	public String printXML(){
		String str = "";
		str = String.format("\t\t<item>");
		str = String.format("%s\n\t\t\t<%s>%s</%s>",str,"ID", ID, "ID");
		str = String.format("%s\n\t\t\t<%s>%s</%s>",str,"manufacturer", manufacturer, "manufacturer");
		str = String.format("%s\n\t\t\t<%s>%d</%s>",str,"quantity", quantity, "quantity");
		str = String.format("%s\n\t\t\t<%s>%s</%s>",str,"availability", availability, "availability");
		str = String.format("%s\n\t\t\t<%s>%f</%s>",str,"price", price, "price");
		str = String.format("%s\n\t\t\t<%s>%d</%s>",str,"minimum_quantity", minimum_quantity, "minimum_quantity");
		str = String.format("%s\n\t\t\t<%s>%s</%s>",str,"packaging", packaging, "packaging");
		str = String.format("%s\n\t\t\t<%s>%s</%s>",str,"series", series, "series");
		str = String.format("%s\n\t\t\t<%s>%f</%s>",str,"resistance_ohms", resistance_ohms, "resistance_ohms");
		str = String.format("%s\n\t\t\t<%s>%d</%s>",str,"tolerance", tolerance, "tolerance");
		str = String.format("%s\n\t\t\t<%s>%f</%s>",str,"power_watts", power_watts, "power_watts");
		str = String.format("%s\n\t\t</item>\n", str);
		return str;
	}
}
