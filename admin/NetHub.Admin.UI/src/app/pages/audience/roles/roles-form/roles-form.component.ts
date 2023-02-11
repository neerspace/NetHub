import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormReady } from 'src/app/components/form/types';
import { PermissionModelExtended, PermissionState, RoleService } from '../role.service';

@Component({
  selector: 'app-roles-form',
  templateUrl: './roles-form.component.html',
  styleUrls: ['./roles-form.component.scss'],
})
export class RolesFormComponent implements OnInit {
  readonly id: number;
  readonly isCreating: boolean;
  ready: FormReady = null;

  constructor(route: ActivatedRoute, private router: Router, public roleService: RoleService) {
    const routeId = route.snapshot.params['id'];
    this.isCreating = routeId === 'create';
    this.id = this.isCreating ? -1 : +routeId;
  }

  ngOnInit(): void {
    this.roleService.onReadyChanges.subscribe(x => (this.ready = x));
    if (this.isCreating) {
      this.roleService.form.ready.setValue('ready');
    } else {
      this.roleService.getAllPermissions();
      this.roleService.getById(this.id);
    }
  }

  backToTable(): void {
    this.router.navigateByUrl('/roles');
  }

  submit(): void {
    if (this.isCreating) {
      // this.roleService.create();
    } else {
      // this.roleService.update(this.id);
    }
  }

  onPermissionChange(event: Event, permission: PermissionModelExtended) {
    const checkbox = event.target as HTMLInputElement;

    if (permission.manageKey) {
      if (!permission.state) {
        permission.state = PermissionState.read;
      } else if (permission.state === PermissionState.read) {
        permission.state = PermissionState.manage;
      } else if (permission.state === PermissionState.manage) {
        permission.state = PermissionState.none;
      }

      if (permission.children) {
        setRecursively(permission.children as any, permission.state);
      }

      function setRecursively(children: PermissionModelExtended[], state: PermissionState) {
        for (const child of children) {
          child.state = state;
          if (child.children) {
            setRecursively(child.children as any, state);
          }
        }
      }

      function nextState(perm: PermissionModelExtended) {
        if (!perm.state) {
          perm.state = 1;
        } else if (perm.state >= 2) {
          perm.state = 0;
        } else {
          perm.state++;
        }
      }
    }
  }
}
