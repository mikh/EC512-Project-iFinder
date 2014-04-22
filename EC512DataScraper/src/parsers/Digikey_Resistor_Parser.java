package parsers;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;

import string_operations.StrOps;
import data_structures.Digikey_Item;

public class Digikey_Resistor_Parser {
	private ArrayList<ArrayList<String>> raw_data = new ArrayList<ArrayList<String>>();
	private ArrayList<Digikey_Item> items = new ArrayList<Digikey_Item>();
	private int last_index;
	
	public void load(ArrayList<ArrayList<String>> data){
		raw_data = data;
	}
	
	public void parse(){
		for(int ii = 0; ii < raw_data.size(); ii++){
			Digikey_Item item = new Digikey_Item();
			String partial = "";
			int index = 0;
			String line = raw_data.get(ii).get(0);
			line = StrOps.trimString(line);
			while(index < line.length() && line.charAt(index) != ' '){
				partial += line.charAt(index++);
			}
			item.ID = partial;
			line = raw_data.get(ii).get(1);
			line = StrOps.trimString(line);
			index = 0;
			partial = getNextWord(line, 0);
			if(!partial.equals(item.ID)){
				System.out.println(String.format("Item %d has unmatched ID tags %s = proper tag, %s = given tag",ii,item.ID,partial));
				System.exit(-1);
			}
			
			partial = getNextWord(line, last_index);
			if(!partial.equals("-")){
				System.out.println(String.format("Item %d has unmatched tags %s = proper tag, %s = given tag",ii,"-",partial));
				System.exit(-1);
			}
			
			String str = "";
			partial = getNextWord(line, last_index);
			int iterations = 0;
			while(StrOps.findPattern(partial, "-ND") == -1 && iterations < 20){
				iterations++;
				str += partial;
				str += " ";
				partial = getNextWord(line, last_index);
			}
			if(iterations == 20){
				System.out.println(String.format("Item %d iterated 20 times.",ii));
				System.exit(-1);
			}
			item.manufacturer = str;
			
			partial = getPreviousWord(line, line.length()-1);
			item.availability = partial;
			partial = getPreviousWord(line, last_index);
			if(!partial.equals("-")){
				System.out.println(String.format("Item %d has unmatched tags %s = proper tag, %s = given tag",ii,"-",partial));
				System.exit(-1);
			}
			partial = getPreviousWord(line, last_index);
			String temp = "";
			for(int jj = 0; jj < partial.length(); jj++){
				if(partial.charAt(jj) != ',')
					temp += partial.charAt(jj);
			}
			partial = temp;
				
			try{
				item.quantity = Integer.parseInt(partial);
			} catch(NumberFormatException e){
				System.out.println(String.format("Item %d raised a number format exception error. String = %s",ii,partial));
				System.exit(-1);
			}
			
			line = raw_data.get(ii).get(2);
			line = StrOps.trimString(line);
			partial = getNextWord(line,0);
			try{
				item.price = Double.parseDouble(partial);
			} catch(NumberFormatException e){
				System.out.println(String.format("Item %d raised a number format exception error. String = %s",ii,partial));
				System.exit(-1);
			}
			
			partial = getNextWord(line,last_index);
			try{
				item.minimum_quantity = Integer.parseInt(partial);
			} catch(NumberFormatException e){
				System.out.println(String.format("Item %d raised a number format exception error. String = %s",ii,partial));
				System.exit(-1);
			}
			
			partial = getNextWord(line,last_index);
			item.packaging = partial;
			
			partial = getNextWord(line,last_index);
			item.series = partial;
			
			partial = getNextWord(line, last_index);
			try{
				while(!(partial.charAt(0) >= '0' && partial.charAt(0) <= '9'))
					partial = getNextWord(line, last_index);
			} catch(StringIndexOutOfBoundsException e){
				System.out.println(String.format("Item %d raised a string index out of bounds exception error. String = %s",ii,partial));
				System.exit(-1);
			}
			try{
				int qq = StrOps.findPattern(partial, "k");
				if(qq != -1){
					temp = "";
					for(int jj = 0; jj < partial.length(); jj++){
						if(partial.charAt(jj) != 'k')
							temp += partial.charAt(jj);
					}
					partial = temp;
				}
				item.resistance_ohms = Double.parseDouble(partial);
				if(qq != -1){
					item.resistance_ohms *= 1000;
					//System.out.println(item.resistance_ohms);
				}
			} catch(NumberFormatException e){
				System.out.println(String.format("Item %d raised a number format exception error. String = %s",ii,partial));
				System.exit(-1);
			}
			
			partial = getNextWord(line, last_index);
			try{
				item.tolerance = Integer.parseInt(partial);
			} catch(NumberFormatException e){
				System.out.println(String.format("Item %d raised a number format exception error. String = %s",ii,partial));
				System.exit(-1);
			}
			
			partial = getNextWord(line, last_index);
			int qq = StrOps.findPattern(partial, "W");
			if(qq != -1){
				temp = "";
				for(int jj = 0; jj < partial.length(); jj++){
					if(partial.charAt(jj) != 'W')
						temp += partial.charAt(jj);
				}
				partial = temp;
			}
			try{
				item.power_watts = Double.parseDouble(partial);
			} catch(NumberFormatException e){
				System.out.println(String.format("Item %d raised a number format exception error. String = %s",ii,partial));
				System.exit(-1);
			}
			
			items.add(item);
		}
	//	for(int jj = 0; jj < items.size(); jj++)
		//	System.out.println(items.get(jj).print());
	}
	
	public String getNextWord(String str, int index){
		String nextWord = "";
		while(index < str.length() && (str.charAt(index) == ' ' || str.charAt(index) == '\t'))
			index++;
		while(index < str.length() && str.charAt(index) != ' ' && str.charAt(index) != '\t')
			nextWord += str.charAt(index++);
		last_index = index;
		return nextWord;
	}
	
	public String getPreviousWord(String str, int index){
		String previousWord = "";
		while(index >= 0 && (str.charAt(index) == ' ' || str.charAt(index) == '\t'))
			index--;
		while(index >= 0 && str.charAt(index) != ' ' && str.charAt(index) != '\t')
			previousWord += str.charAt(index--);
		
		String temp = "";
		for(int ii = previousWord.length()-1; ii >= 0; ii--)
			temp += previousWord.charAt(ii);
		previousWord = temp;
		last_index = index;
		return previousWord;
	}
	
	public void printXML(String file){
		try{
			BufferedWriter bw = new BufferedWriter(new FileWriter(file, false));
			bw.write("<data>\n");
			bw.write("\t<ProductType>electronics</ProductType>\n");
			bw.write("\t<ProductName>resistor</ProductName>\n");
			bw.write("\t<items>\n");
			for(int ii = 0; ii < items.size(); ii++)
				bw.write(items.get(ii).printXML());
			bw.write("\t</items>\n");
			bw.write("</data>\n");
			bw.close();
		} catch(IOException e){
			System.out.println(String.format("Digikey_Resistor_Parser::printXML - IOException when trying to write to file %s.", file));
			System.exit(-1);
		}
	}
	
	public void printFilterXML(String file){
		try{
			BufferedWriter bw = new BufferedWriter(new FileWriter(file, false));
			bw.write("<filters>\n");
			bw.write("\t<electronics>\n");
			bw.write("\t\t<resistors>\n");
			
			//manufacturers
			ArrayList<String> fields = new ArrayList<String>();
			for(int ii = 0; ii < items.size(); ii++){
				String mm = items.get(ii).manufacturer;
				if(!fields.contains(mm))
					fields.add(mm);
			}
			bw.write("\t\t\t<manufacturer>\n");
			for(int ii = 0; ii < fields.size(); ii++){
				bw.write(String.format("\t\t\t\t<item>\n\t\t\t\t\t%s\n\t\t\t\t</item>\n", fields.get(ii)));
			}
			bw.write("\t\t\t</manufacturer>\n");
			
			//quantity
			int high = 0;
			int low = 99999999;
			for(int ii = 0; ii < items.size(); ii++){
				int val = items.get(ii).quantity;
				if(val > high) high = val;
				if(val < low) low = val;
			}
			bw.write("\t\t\t<quantity>\n");
			bw.write(String.format("\t\t\t\t<high>\n\t\t\t\t\t%d\n\t\t\t\t</high>\n", high));
			bw.write(String.format("\t\t\t\t<low>\n\t\t\t\t\t%d\n\t\t\t\t</low>\n", low));
			bw.write("\t\t\t</quantity>\n");
			
			//availablity
			fields = new ArrayList<String>();
			for(int ii = 0; ii < items.size(); ii++){
				String mm = items.get(ii).availability;
				if(!fields.contains(mm))
					fields.add(mm);
			}
			bw.write("\t\t\t<availability>\n");
			for(int ii = 0; ii < fields.size(); ii++){
				bw.write(String.format("\t\t\t\t<item>\n\t\t\t\t\t%s\n\t\t\t\t</item>\n", fields.get(ii)));
			}
			bw.write("\t\t\t</availability>\n");
			
			//price
			double high_f = 0;
			double low_f = 99999999;
			for(int ii = 0; ii < items.size(); ii++){
				double val = items.get(ii).price;
				if(val > high_f) high_f = val;
				if(val < low_f) low_f = val;
			}
			bw.write("\t\t\t<price>\n");
			bw.write(String.format("\t\t\t\t<high>\n\t\t\t\t\t%f\n\t\t\t\t</high>\n", high_f));
			bw.write(String.format("\t\t\t\t<low>\n\t\t\t\t\t%f\n\t\t\t\t</low>\n", low_f));
			bw.write("\t\t\t</price>\n");
			
			//minimum_quantity
			high = 0;
			low = 99999999;
			for(int ii = 0; ii < items.size(); ii++){
				int val = items.get(ii).minimum_quantity;
				if(val > high) high = val;
				if(val < low) low = val;
			}
			bw.write("\t\t\t<minimum_quantity>\n");
			bw.write(String.format("\t\t\t\t<high>\n\t\t\t\t\t%d\n\t\t\t\t</high>\n", high));
			bw.write(String.format("\t\t\t\t<low>\n\t\t\t\t\t%d\n\t\t\t\t</low>\n", low));
			bw.write("\t\t\t</minimum_quantity>\n");
			
			//packaging
			fields = new ArrayList<String>();
			for(int ii = 0; ii < items.size(); ii++){
				String mm = items.get(ii).packaging;
				if(!fields.contains(mm))
					fields.add(mm);
			}
			bw.write("\t\t\t<packaging>\n");
			for(int ii = 0; ii < fields.size(); ii++){
				bw.write(String.format("\t\t\t\t<item>\n\t\t\t\t\t%s\n\t\t\t\t</item>\n", fields.get(ii)));
			}
			bw.write("\t\t\t</packaging>\n");
			
			//series
			fields = new ArrayList<String>();
			for(int ii = 0; ii < items.size(); ii++){
				String mm = items.get(ii).series;
				if(!fields.contains(mm))
					fields.add(mm);
			}
			bw.write("\t\t\t<series>\n");
			for(int ii = 0; ii < fields.size(); ii++){
				bw.write(String.format("\t\t\t\t<item>\n\t\t\t\t\t%s\n\t\t\t\t</item>\n", fields.get(ii)));
			}
			bw.write("\t\t\t</series>\n");
			
			//resistance
			high_f = 0;
			low_f = 99999999;
			for(int ii = 0; ii < items.size(); ii++){
				double val = items.get(ii).resistance_ohms;
				if(val > high_f) high_f = val;
				if(val < low_f) low_f = val;
			}
			bw.write("\t\t\t<resistance_ohms>\n");
			bw.write(String.format("\t\t\t\t<high>\n\t\t\t\t\t%f\n\t\t\t\t</high>\n", high_f));
			bw.write(String.format("\t\t\t\t<low>\n\t\t\t\t\t%f\n\t\t\t\t</low>\n", low_f));
			bw.write("\t\t\t</resistance_ohms>\n");
			
			//tolerance
			high = 0;
			low = 99999999;
			for(int ii = 0; ii < items.size(); ii++){
				int val = items.get(ii).tolerance;
				if(val > high) high = val;
				if(val < low) low = val;
			}
			bw.write("\t\t\t<tolerance>\n");
			bw.write(String.format("\t\t\t\t<high>\n\t\t\t\t\t%d\n\t\t\t\t</high>\n", high));
			bw.write(String.format("\t\t\t\t<low>\n\t\t\t\t\t%d\n\t\t\t\t</low>\n", low));
			bw.write("\t\t\t</tolerance>\n");
			
			//power
			high_f = 0;
			low_f = 99999999;
			for(int ii = 0; ii < items.size(); ii++){
				double val = items.get(ii).power_watts;
				if(val > high_f) high_f = val;
				if(val < low_f) low_f = val;
			}
			bw.write("\t\t\t<power_watts>\n");
			bw.write(String.format("\t\t\t\t<high>\n\t\t\t\t\t%f\n\t\t\t\t</high>\n", high_f));
			bw.write(String.format("\t\t\t\t<low>\n\t\t\t\t\t%f\n\t\t\t\t</low>\n", low_f));
			bw.write("\t\t\t</power_watts>\n");
			
			
			bw.write("\t\t</resistors>\n");
			bw.write("\t</electronics>\n");
			bw.write("</filters>\n");
			bw.close();
		} catch(IOException e){
			System.out.println(String.format("Digikey_Resistor_Parser::printFilterXML - IOException when trying to write to file %s.", file));
			System.exit(-1);
		}
	}
	
	public void printRaw(){
		for(int ii = 0; ii < raw_data.size(); ii++){
			for(int jj = 0; jj < raw_data.get(ii).size(); jj++)
				System.out.println(raw_data.get(ii).get(jj));
			System.out.print("\n");
		}
	}
	public void printRaw(String file){
		try{
			BufferedWriter bw = new BufferedWriter(new FileWriter(file, false));
			for(int ii = 0; ii < raw_data.size(); ii++){
				for(int jj = 0; jj < raw_data.get(ii).size(); jj++)
					bw.write(raw_data.get(ii).get(jj) + "\n");
				bw.write("\n");
			}
			bw.close();
		} catch(IOException e){
			System.out.println(String.format("Digikey_Resistor_Parser::printRaw - IOException when trying to write to file %s.", file));
			System.exit(-1);
		}
	}
	
	public void printProcessed(){
		for(int ii = 0; ii < items.size(); ii++){
			System.out.println(items.get(ii).print());
		}
	}
	public void printProcessed(String file){
		try{
			BufferedWriter bw = new BufferedWriter(new FileWriter(file, false));
			for(int ii = 0; ii < items.size(); ii++){
				bw.write(items.get(ii).print());
			}
			bw.close();
		} catch(IOException e){
			System.out.println(String.format("Digikey_Resistor_Parser::printProcessed - IOException when trying to write to file %s.", file));
			System.exit(-1);
		}
	}
}
