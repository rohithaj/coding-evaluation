import Position from './Position';
import Name from './Name';
import Employee from './Employee';

abstract class Organization {
  private root: Position;
  private identifier:number=1;
  constructor() {
    this.root = this.createOrganization();
  }

  protected abstract createOrganization(): Position;

  printOrganization = (position: Position, prefix: string): string => {
    let str = `${prefix}+-${position}\n`;
    for (const p of position.getDirectReports()) {
      str = str.concat(this.printOrganization(p, `${prefix}  `));
    }
    return str;
  };

  // Hire the given person as an employee in the position that has that title
  // Return the newly filled position or undefined if no position has that title
  hire(person: Name, title: string): Position | undefined {
    // your code here
   return this.hireRecursion(person,title,this.root);
  }

  private hireRecursion(person: Name, title: string,position:Position):Position | undefined 
  {
    if(position.getTitle()===title)
    {
      position.setEmployee(new Employee(this.identifier++,person));
      return position;
    }
    position.getDirectReports().forEach(element => {
      var result= this.hireRecursion(person,title,element);
      if(result!=undefined)
       return result;
    });
    return undefined;
  }

  toString = () => this.printOrganization(this.root, '');
}

export default Organization;
