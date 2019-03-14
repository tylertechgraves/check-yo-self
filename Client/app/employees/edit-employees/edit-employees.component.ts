import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'appc-edit-employees',
  templateUrl: './edit-employees.component.html',
  styleUrls: ['./edit-employees.component.scss']
})
export class EditEmployeesComponent implements OnInit {
  @Input() editFormGroup: FormGroup;

  constructor() { }

  ngOnInit() {
  }

}
