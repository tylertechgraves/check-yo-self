import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'appc-add-employees',
  templateUrl: './add-employees.component.html',
  styleUrls: ['./add-employees.component.scss']
})
export class AddEmployeesComponent implements OnInit {
  @Input() addFormGroup: FormGroup;

  constructor() { }

  ngOnInit() {
  }

}
